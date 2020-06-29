using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Areas.User.Models;
using RealEstateWebsite.Models;
namespace RealEstateWebsite.Areas.User.Controllers
{
    public class AccountController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(string username, string password)
        {
            Customer cus = db.Customers.SingleOrDefault(n => n.Account.UserName == username && n.Account.Role_Account.FirstOrDefault().Role.Role_Name == "Customer");

            if (cus == null)
            {
                ViewBag.Error = "Username or password is incorrect";
            }
            else
            {
                if (!HashPwdTool.CheckPassword(password, cus.Account.PasswordHash))
                {
                    ViewBag.Error = "Username or password is incorrect";
                }
                else
                {
                    if(cus.Blocks != null && (cus.Blocks.LastOrDefault().UnBlockDate == null || cus.Blocks.LastOrDefault().UnBlockDate > DateTime.Now))
                    {
                        ViewBag.Error = "Username is blocking";
                    }
                    else
                    {
                        Session["Account"] = cus.Account;
                        AccountLog accLog = new AccountLog();
                        accLog.Account = cus.Account;
                        db.AccountLogs.Add(accLog);
                        db.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View("Login");
        }
        public bool CheckUserName(string userName)
        {
            return db.Accounts.Count(n => n.UserName == userName) > 0;
        }

        [HttpPost]
        public ActionResult SignUp(SignUpModel cst, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                //db.Customers.Add(cst);
                //db.SaveChanges();
                if (CheckUserName(cst.UserName))
                {
                    ModelState.AddModelError("", "Username already exists");
                }
                else
                {
                    //string a = HashPwdTool.GeneratePassword("1");
                    var account = new Account();/* { UserName = "a",PasswordHash=a};*/
                    account.UserName = cst.UserName;
                    account.PasswordHash = HashPwdTool.GeneratePassword(cst.PassWord);
                    var phonenumber = new PhoneNumber();
                    phonenumber.PhoneNumber1 = cst.PhoneNumber;
                    var customer = new Customer();
                    customer.Address = cst.Address;
                    customer.Email = cst.Email;
                    customer.Firstname = cst.FirstName;
                    customer.LastName = cst.LastName;

                    var fileName2 = Path.GetFileName(fileUpload.FileName);
                    //Lưu đường dẫn của file
                    var path2 = Path.Combine(Server.MapPath("~/Images/Customer"), fileName2);
                    if (System.IO.File.Exists(path2))
                    {
                        ViewBag.ThongBao = "Images already exists";
                    }
                    else
                    {
                        fileUpload.SaveAs(path2);
                    }
                    customer.Avatar_URL = fileUpload.FileName;
                    customer.Account = account;

                    Role_Account r_acc = new Role_Account();
                    r_acc.Account = account;
                    r_acc.Role_ID = 3;

                    customer.PhoneNumbers.Add(phonenumber);
                    db.Customers.Add(customer);
                    db.Role_Account.Add(r_acc);
                    db.SaveChanges();
                    ViewBag.ThongBao = "Signup succcessful";
                }
            }
            return View("SignUp");
        }

        public ActionResult Logout(FormCollection f)
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public PartialViewResult UpdatePassPartial()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult UpdatePassPartial(string passwordM, string passwordC, string passwordL)
        {
            RealEstateWebsiteEntities dbs = new RealEstateWebsiteEntities();
            Account cst = Session["Account"] as Account;
            if (HashPwdTool.CheckPassword(passwordC, cst.PasswordHash) && passwordM == passwordL)
            {
                dbs.spUpdateAccount(cst.Account_ID, HashPwdTool.GeneratePassword(passwordM));
                ViewBag.ThongBao = "Change password successful";
            }
            else
            {
                ViewBag.ThongBao = "Current pass incorrect";
            }
            return PartialView("UpdatePassPartial");
        }
    }
}