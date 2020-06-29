using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;

namespace RealEstateWebsite.Areas.Censor.Controllers
{
    public class AccountController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();

        // GET: Employee/Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Employee emp = db.Employees.SingleOrDefault(n => n.Account.UserName == username && n.Account.Role_Account.FirstOrDefault().Role.Role_Name == "Censor");

            if (emp == null)
            {
                ViewBag.Error = "Username or password is incorrect";
                return View("Login");
            }

            if (emp.Block1.LastOrDefault() != null && (emp.Block1.LastOrDefault().UnBlockDate == null || emp.Block1.LastOrDefault().UnBlockDate > DateTime.Now))
            {
                ViewBag.Error = "Account was blocked";
                return View("Login");
            }

            if (emp.Quits.LastOrDefault() != null)
            {
                ViewBag.Error = "Account was quited";
                return View("Login");
            }

            if (emp != null && HashPwdTool.CheckPassword(password, emp.Account.PasswordHash))
            {
                Session["Account_Censor"] = emp;

                AccountLog accLog = new AccountLog();
                accLog.Account = emp.Account;

                db.AccountLogs.Add(accLog);

                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Cannot connect to server. Please try again!";
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}