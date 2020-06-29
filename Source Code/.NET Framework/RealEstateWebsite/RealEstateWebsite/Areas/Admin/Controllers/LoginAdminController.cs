using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;

using RealEstateWebsite.Areas.Admin.Models;

namespace RealEstateWebsite.Areas.Admin.Controllers
{
   
    public class LoginAdminController : Controller
    {
        // GET: Admin/LoginAdmin
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login(FormCollection frm)
        {
            string UserName = frm["username"].ToString();
            Account acc = db.Accounts.Where(p => p.UserName == UserName && p.Role_Account.FirstOrDefault().Role_ID == 1).SingleOrDefault();

            if (acc != null)
            {
                bool Pass = HashPwdTool.CheckPassword(frm["password"].ToString(), acc.PasswordHash);
                if (Pass)
                {
                    Employee emp = db.Employees.Where(p => p.Account.Account_ID == acc.Account_ID).SingleOrDefault();
                    if (emp.Quits == null && (emp.Block1.LastOrDefault() == null || (emp.Block1.LastOrDefault() != null && (emp.Block1.LastOrDefault().UnBlockDate == null || emp.Block1.LastOrDefault().UnBlockDate <= DateTime.Now))))
                    {
                        db.USP_InsertAccountLog(acc.Account_ID);
                        Session["AccountUser"] = UserName;
                        Session["ID_User"] = emp.Employee_ID;
                        Session["ID_Acc"] = acc.Account_ID;
                        Session["Avatar"] = "/Images/Employee/" + emp.Avatar_URL;

                        return RedirectToAction("Index", "Home");
                    }

                    else
                    {
                        Block1 bl = db.Block1.Where(p => p.Employee.Employee_ID == emp.Employee_ID).OrderByDescending(p => p.ModifiedDate).FirstOrDefault();
                        if (bl != null)
                        {
                            if (bl.UnBlockDate <= DateTime.Now)
                            {
                                BlockEmployee.UnBlockEmp(emp.Employee_ID);
                                db.USP_InsertAccountLog(acc.Account_ID);
                                Session["AccountUser"] = UserName;
                                Session["ID_User"] = emp.Employee_ID; Session["Avatar"] = "/Images/Employee/" + emp.Avatar_URL;
                                Session["ID_Acc"] = acc.Account_ID;
                                return RedirectToAction("Index", "Home");
                            }
                            else

                                ModelState.AddModelError("", "Login Failed! Account is lock");
                        }
                        else
                        {
                            db.USP_InsertAccountLog(acc.Account_ID);
                            Session["AccountUser"] = UserName;
                            Session["ID_User"] = emp.Employee_ID; Session["Avatar"] = "/Images/Employee/" + emp.Avatar_URL;
                            Session["ID_Acc"] = acc.Account_ID;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }



                else
                {
                    ModelState.AddModelError("", "Login Failed! Username or Password is wrong");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login Failed! Account is not Admin");
            }
            return View();

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "LoginAdmin");
        }
    }
}