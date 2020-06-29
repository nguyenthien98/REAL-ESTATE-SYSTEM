using System;
using System.Linq;
using System.Web.Mvc;
using RealEstateWebsite.Models;
using RealEstateWebsite.Areas.Admin.Models;
using System.IO;
using PagedList;
//using RealEstateWebsite.Areas.Admin.Models;

namespace RealEstateWebsite.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();

        //GET: Admin/Customer
        //public ActionResult Viewcustomer()
        //{
        //    var lstCustomer = db.Customers.ToList();
        //    return View(lstCustomer);
        //}
        public ActionResult Viewcustomer(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var lstCustomer = db.Customers.ToList().OrderBy(p=>p.Customer_ID).ToPagedList(pageNumber, pageSize);
            return View(lstCustomer);
        }

        public ActionResult ReportedCustomer()
        {
            var lstCustomer = db.Customer_Report.Where(n => n.Status == 0).ToList();
            return View(lstCustomer);
        }

        public ViewResult CreateCustomer()
        {
            var TypeList = db.Types.Select(p => p.Type_Name).ToList();
            ViewBag.roleList = TypeList;

            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer customer, string TypeAcc, string phoneNumber, string password)
        {
            var TypeList = db.Types.Select(p => p.Type_Name).ToList();
            ViewBag.roleList = TypeList;

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

                        string path = Path.Combine(Server.MapPath(Constants.CUS_IMG_URL_ADD), pic);
                        customer.Avatar_URL = pic;

                        file.SaveAs(path);
                    }
                }
                else
                {
                    customer.Avatar_URL = Constants.CUS_IMG_NOAVATAR;
                }

                customer.Account.PasswordHash = HashPwdTool.GeneratePassword(password);

                PhoneNumber phNum = new PhoneNumber();
                phNum.PhoneNumber1 = phoneNumber;
                customer.PhoneNumbers.Add(phNum);

                Role_Account ra = new Role_Account();
                ra.Role_ID = 3;
                customer.Account.Role_Account.Add(ra);

                int type = db.Types.Where(p => p.Type_Name == TypeAcc).Select(r => r.Type_ID).SingleOrDefault();
                RealEstateWebsite.Models.Type t = db.Types.Find(type);
                customer.Type = db.Types.Find(t.Type_ID);

                db.Customers.Add(customer);

                db.SaveChanges();
                return RedirectToAction("Viewcustomer", "Customer");
            }
            return View();
        }

        [HttpPost]
        public ActionResult BlockCust(int idCust, string Reason, DateTime? unBlockDate)
        {
            if (Reason != null && Reason != "")
            {
                string a = Session["ID_User"].ToString();
                int idEmp = Convert.ToInt32(a);
                BlockCustomer.BlockCust(idCust, Reason, idEmp, unBlockDate);
                return RedirectToAction("Viewcustomer", "Customer");
            }
            else
                ModelState.AddModelError("", "Block Failed! Reason is empty");

            return RedirectToAction("Viewcustomer", "Customer");

        }
        [HttpPost]
        public ActionResult UnBlockCust(int idCust)
        {

            BlockCustomer.UnBlockCust(idCust);
            return RedirectToAction("Viewcustomer", "Customer");

        }

        [HttpPost]
        public ActionResult Dismiss(int idCust)
        {
            //string a = Session["ID_User"].ToString();
            //int idEmp = Convert.ToInt32(a);
            Employee emp = db.Employees.Find(Session["ID_User"]);
            Customer_Report customerR = db.Customer_Report.Find(idCust);
            customerR.Status = 1;
            customerR.Employee = db.Employees.Find(emp.Employee_ID);
            db.SaveChanges();
            return RedirectToAction("ReportedCustomer", "Customer");

        }

        public ActionResult EditCustomer(int customer_id)
        {
            var TypeList = db.Types.Select(p => p.Type_Name).ToList();
            ViewBag.roleList = TypeList;

            Customer customer = db.Customers.Where(p => p.Customer_ID == customer_id).SingleOrDefault();

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCustomer(Customer customer, string TypeAcc, string phoneNumber)
        {
            var TypeList = db.Types.Select(p => p.Type_Name).ToList();

            ViewBag.roleList = TypeList;
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

                            string path = Path.Combine(Server.MapPath(Constants.CUS_IMG_URL_ADD), pic);
                            customer.Avatar_URL = pic;

                            file.SaveAs(path);
                        }
                    }
                    else
                    {
                        customer.Avatar_URL = Constants.CUS_IMG_NOAVATAR;
                    }



                    Customer customerDB = db.Customers.Find(customer.Customer_ID);
                    customerDB.Firstname = customer.Firstname;
                    customerDB.LastName = customer.LastName;

                    int type = db.Types.Where(p => p.Type_Name == TypeAcc).Select(r => r.Type_ID).SingleOrDefault();
                    RealEstateWebsite.Models.Type t = db.Types.Find(type);
                    customerDB.Type = db.Types.Find(t.Type_ID);

                    if (phoneNumber != "")
                    {
                        if (phoneNumber != null)
                        {

                            if ((customerDB.PhoneNumbers.LastOrDefault() != null) && (customerDB.PhoneNumbers.LastOrDefault().PhoneNumber1 != phoneNumber))
                            {
                                customerDB.PhoneNumbers.Remove(customerDB.PhoneNumbers.Last());

                                PhoneNumber pn = new PhoneNumber();
                                pn.PhoneNumber1 = phoneNumber;
                                pn.ModifiedDate = DateTime.Now;

                                customerDB.PhoneNumbers.Add(pn);
                            }
                        }
                    }
                    customerDB.Email = customer.Email;
                    customerDB.Address = customer.Address;

                    db.SaveChanges();

                    TempData["SaveOK"] = "OK";

                    return RedirectToAction("Viewcustomer", "Customer");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Save Failed! Try again later");
            }
            return View();
        }
        public ActionResult ViewAccount(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var lst = db.Accounts.Where(p => p.Role_Account.FirstOrDefault().Role_ID == 3).ToList().OrderBy(p => p.Account_ID).ToPagedList(pageNumber, pageSize);
            return View(lst);
        }
        public ActionResult ResetPass(int iDEmp, string password1)
        {
            Account acc = db.Accounts.Where(p => p.Account_ID == iDEmp).SingleOrDefault();
            acc.PasswordHash = HashPwdTool.GeneratePassword(password1);
            db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ViewAccount", "Customer");
        }
        public ActionResult ChangePassAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassAccount(int idAcc, string password1, string pass)
        {
            Account acc = db.Accounts.Where(p => p.Account_ID == idAcc).SingleOrDefault();
            bool c = HashPwdTool.CheckPassword(pass, acc.PasswordHash);
            if (c)
            {
                try
                {
                    acc.PasswordHash = HashPwdTool.GeneratePassword(password1);
                    db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.changeAccSucc = "successful!";
                }
                catch { ViewBag.changeAccFail = "Fail!"; }

            }

            else
            {
                ModelState.AddModelError("", "Edit Failed! Password is wrong");
            }
            return View();
        }
    }
}