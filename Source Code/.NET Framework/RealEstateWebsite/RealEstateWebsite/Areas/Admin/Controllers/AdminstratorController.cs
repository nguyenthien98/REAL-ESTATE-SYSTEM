using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;
using RealEstateWebsite.Areas.Admin.Models;
using System.Web.UI;
using System.IO;
using PagedList;

namespace RealEstateWebsite.Areas.Admin.Controllers
{
    public class AdminstratorController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
       
        // GET: Admin/Adminstrator
        public ActionResult Viewemp(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            string a=  Session["ID_User"].ToString();
            int id = Convert.ToInt32(a);
            return View(db.Employees.Where(p=>p.Employee2.Employee_ID==id).ToList().OrderBy(p=>p.Employee_ID).ToPagedList(pageNumber, pageSize));
         

        }
       

        public ActionResult CreateEmp()
        {
            var roleList = db.Roles.Select(p => p.Role_Name).Where(p => p != "Admin" && p != "SuperAdmin" && p != "Customer").ToList();

            ViewBag.roleList = roleList;
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmp(Employee emp, string RoleAcc, string manager_id, string password1)

        {

            var roleList = db.Roles.Select(p => p.Role_Name).Where(p => p != "Admin" && p != "SuperAdmin").ToList();

            ViewBag.roleList = roleList;
            if (ModelState.IsValid)
            {

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[Request.Files.Count - 1];

                    if (file != null)
                    {
                        string pic = Path.GetFileName(file.FileName);

                        string extensionFileName = CommonFunction.getExtensionFileName(pic);

                        pic = CommonFunction.hashSHA256(pic) + extensionFileName;

                        string path = Path.Combine(Server.MapPath(Constants.EMP_IMG_URL_ADD), pic);
                        emp.Avatar_URL = pic;

                        file.SaveAs(path);
                    }
                }
                else
                {
                    emp.Avatar_URL = Constants.EMP_IMG_NOAVATAR;
                }


  
                emp.Account.PasswordHash = HashPwdTool.GeneratePassword(password1);

                int role = db.Roles.Where(p => p.Role_Name == RoleAcc).Select(r => r.Role_ID).SingleOrDefault();
                Role_Account ra = new Role_Account();
                ra.Role_ID = role;
                emp.Account.Role_Account.Add(ra);
                db.Employees.Add(emp);
                db.SaveChanges();
                int id = db.Employees.Where(p => p.Account.UserName == emp.Account.UserName).SingleOrDefault().Employee_ID;
                db.USP_AddManager_id(id, Convert.ToInt32(manager_id));
                return RedirectToAction("ViewEmp", "Adminstrator");

            }
            return View();
        }
        public ActionResult ProfileEmp(int idemp)
        {
            Employee model = db.Employees.Where(p=>p.Employee_ID==idemp).SingleOrDefault();
            return View(model);
        }
        public ActionResult EditEmp(int idemp)
        {
            Employee model = ViewEmp.GetOneEmp(idemp);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmp(Employee model)
        {
            try
            {
                if (ModelState.IsValid)
                { 
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[Request.Files.Count - 1];

                        if (file != null)
                        {
                            string pic = Path.GetFileName(file.FileName);

                            string extensionFileName = CommonFunction.getExtensionFileName(pic);

                            pic = CommonFunction.hashSHA256(pic) + extensionFileName;

                            string path = Path.Combine(Server.MapPath(Constants.EMP_IMG_URL_ADD), pic);
                            model.Avatar_URL = pic;

                            file.SaveAs(path);
                        }
                    }
                 
                
                    ViewEmp.UpdateEmp(model);
                    return RedirectToAction("ViewEmp", "Adminstrator");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Save Failed! Try again later");
            }



            return View(model);
        }

        [HttpPost]
        public ActionResult BlockEmp(int iDEmp, string reasonBlockEmp, DateTime? dateUnBlock)
        {
            if (reasonBlockEmp != null && reasonBlockEmp != "")
            {
                BlockEmployee.BlockEmp(iDEmp, reasonBlockEmp, dateUnBlock);
                return RedirectToAction("ViewEmp", "Adminstrator");
            }
            else
                ModelState.AddModelError("", "Block Failed! Reason is empty");

            return RedirectToAction("ViewEmp", "Adminstrator");

        }
        [HttpPost]
        public ActionResult UnBlockEmp(int iDEmp)
        {

            BlockEmployee.UnBlockEmp(iDEmp);
            return RedirectToAction("ViewEmp", "Adminstrator");

        }
        [HttpPost]
        public ActionResult QuitEmp(int iDEmp, string reasonBlockEmp)
        {
            try
            {
                QuitEmployee.QuitEmp(iDEmp, reasonBlockEmp);
                return RedirectToAction("ViewEmp", "Adminstrator");
            }
            catch
            {
                ModelState.AddModelError("", "Quit Failed! Try again later");
            }
            return RedirectToAction("ViewEmp", "Adminstrator");
        }


        public ActionResult ViewAccount(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var lst = db.Accounts.Where(p => p.Role_Account.FirstOrDefault().Role_ID != 3 && p.Role_Account.FirstOrDefault().Role_ID != 1).ToList().OrderBy(p => p.Account_ID).ToPagedList(pageNumber, pageSize);
            return View(lst);
        }
        public ActionResult ResetPass(int iDEmp, string password1)
        {
            Account acc = db.Accounts.Where(p => p.Account_ID == iDEmp).SingleOrDefault();
            acc.PasswordHash = HashPwdTool.GeneratePassword(password1);
            db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ViewAccount", "Adminstrator");
        }
        public ActionResult ChangePassAccount()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassAccount(int idAcc, string password1,string pass)
        {
            Account acc = db.Accounts.Where(p => p.Account_ID == idAcc).SingleOrDefault();
            bool c = HashPwdTool.CheckPassword(pass,acc.PasswordHash);
            if(c)
            {
                try
                {
                    acc.PasswordHash = HashPwdTool.GeneratePassword(password1);
                    db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.changeAccSucc = "successful!";
                } catch { ViewBag.changeAccFail = "Fail!"; }
               
            }
          
            else
            {
                ModelState.AddModelError("", "Edit Failed! Password is wrong");
            }
            return View();
        }

    }
}