using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;

namespace RealEstateWebsite.Areas.Censor.Controllers
{
    public class CustomerController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();


        public ViewResult Index()
        {
            var lstCustomer = db.Customers.ToList();

            if (TempData["SaveOK"] != null && TempData["SaveOK"].ToString() != "")
            {
                ViewBag.SaveOK = TempData["SaveOK"].ToString();
            }

            return View(lstCustomer);
        }

        public ViewResult Search(int typeOfSearch, string value)
        {
            if (TempData["SaveOK"] != null && TempData["SaveOK"].ToString() != "")
            {
                ViewBag.SaveOK = TempData["SaveOK"].ToString();
            }
            
            List<Customer> lstCustomer = null;

            if(typeOfSearch == 1)
            {
                lstCustomer = db.Customers.Where(n => (n.LastName + " " + n.Firstname).ToString().Contains(value)).ToList(); 
            } else if(typeOfSearch == 2)
            {
                lstCustomer = db.Customers.Where(n => n.Account.UserName.ToString().Contains(value)).ToList();
            } else
            {
                lstCustomer = db.Customers.Where(n => n.Email.ToString().Contains(value)).ToList();
            }

            ViewBag.TypeOfSearch = typeOfSearch;
            ViewBag.Value = value;

            return View("Index", lstCustomer);
        }

        public ViewResult ReportedCustomers()
        {
            var lstCustomer = db.Customer_Report.Where(n => n.Status == 0).ToList();
            return View(lstCustomer);
        }

        public string BlockCus_Report(string cus_report_id)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    Customer_Report cr = db.Customer_Report.Single(n => n.Customer_Report_ID.ToString() == cus_report_id);

                    string result = BlockCustomer(cr.Customer.Customer_ID.ToString());
                    if (result == "0")
                    {
                        trans.Rollback();
                        return "0";
                    }

                    Employee censor = Session["Account_Censor"] as Employee;

                    cr.Status = 1;
                    cr.Employee = db.Employees.Find(censor.Employee_ID);

                    db.SaveChanges();

                    trans.Commit();

                    return "1";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    trans.Rollback();
                    return "0";
                }
            }
        }

        public string DeleteReport(string cus_report_id)
        {
            try
            {
                Customer_Report cr = db.Customer_Report.Single(n => n.Customer_Report_ID.ToString() == cus_report_id);

                Employee censor = Session["Account_Censor"] as Employee;

                cr.Status = 1;
                cr.Employee = db.Employees.Find(censor.Employee_ID);

                db.SaveChanges();

                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }

        [HttpGet]
        public ViewResult AddCustomer()
        {
            if (TempData["AddCustomerOK"] != null && TempData["AddCustomerOK"].ToString() != "")
            {
                ViewBag.AddCustomerOK = TempData["AddCustomerOK"].ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer, string phoneNumber, string password)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
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

                    db.Customers.Add(customer);

                    db.SaveChanges();
                    trans.Commit();

                    TempData["AddCustomerOK"] = "OK";

                    return RedirectToAction("AddCustomer");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    trans.Rollback();
                    return View();
                }
            }
        }

        public ActionResult Details(string customerreport_id)
        {
            Customer_Report customer = db.Customer_Report.FirstOrDefault(n => n.Customer_Report_ID.ToString() == customerreport_id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return PartialView("DetailsPartialView", customer);

        }
        public string BlockCustomer(string customer_id/*, string reason*/)
        {

            try
            {
                Customer customer = db.Customers.Single(n => n.Customer_ID.ToString() == customer_id);

                if (customer == null)
                {
                    RedirectToAction("Error_404", "ErrorPage");
                    return "0";
                }
                Block ps = new Block();
                ps.Customer = customer;
                Employee censor = Session["Account_Censor"] as Employee;
                ps.Employee = db.Employees.Find(censor.Employee_ID);
                ps.Reason = "Employee " + censor.Employee_ID + " was blocked";
                db.Blocks.Add(ps);
                db.SaveChanges();
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public string UnBlockCustomer(string customer_id)
        {
            try
            {
                Customer customer = db.Customers.Single(n => n.Customer_ID.ToString() == customer_id);

                if (customer == null)
                {
                    RedirectToAction("Error_404", "ErrorPage");
                    return "0";
                }

                customer.Blocks.Last().UnBlockDate = DateTime.Now;

                db.SaveChanges();
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public ActionResult EditPartialView(string customer_id)
        {
            Customer customer = db.Customers.FirstOrDefault(n => n.Customer_ID.ToString() == customer_id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return PartialView("EditPartialView", customer);
        }

        [HttpPost]
        public ActionResult SaveCustomer(Customer customer, string phonenumber)
        {
            Customer customerDB = db.Customers.Find(customer.Customer_ID);
            try
            {
                customerDB.Firstname = customer.Firstname;
                customerDB.LastName = customer.LastName;
                PhoneNumber pn = new PhoneNumber();
                pn.PhoneNumber1 = phonenumber;
                if (customerDB.PhoneNumbers.LastOrDefault() != null)
                {
                    customerDB.PhoneNumbers.Remove(customerDB.PhoneNumbers.Last());
                }
                customerDB.PhoneNumbers.Add(pn);

                customerDB.Email = customer.Email;
                customerDB.Address = customer.Address;

                db.SaveChanges();

                TempData["SaveOK"] = "OK";
                return RedirectToAction("Index");
            }
            catch (OptimisticConcurrencyException)
            {
                db.Entry(customerDB).Reload();
                if (db.Entry(customerDB).State == EntityState.Detached)
                {
                    TempData["SaveError"] = "Customer has been deleted by another user";
                }
                else
                {
                    TempData["SaveError"] = "Customer has been updated by another user";
                }
            }
            catch (Exception ex)
            {
                TempData["SaveError"] = "Somethings was wrong. Please try again.";
            }

            return RedirectToAction("Index");
        }
    }
}