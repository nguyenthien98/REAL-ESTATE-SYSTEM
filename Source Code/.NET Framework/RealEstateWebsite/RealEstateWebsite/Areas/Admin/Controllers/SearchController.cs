using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;

namespace RealEstateWebsite.Areas.Admin.Controllers
{
    public class SearchController : Controller
    {
        // GET: Admin/Search
        public ActionResult SearchResultEmployee(string key)
        {
            RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
            List<Employee> list = db.Employees.Where(p => p.FullName.Contains(key) || p.Account.UserName.Contains(key)).ToList();
            if (list.Count != 0)
            {
                ViewBag.ThongBaoSearch = "Found " + list.Count + " results";
            }
            else
                ViewBag.ThongBaoSearchNotFound = "No Results Found";

            return View(list);
        }

        public ActionResult SearchResultCustomer(string key)
        {
            RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
            List<Customer> list = db.Customers.Where(p => p.Firstname.Contains(key) || p.LastName.Contains(key) || p.Account.UserName.Contains(key)).ToList();
            if (list.Count != 0)
            {
                ViewBag.ThongBaoSearch = "Found " + list.Count + " results";
            }
            else
                ViewBag.ThongBaoSearchNotFound = "No Results Found";

            return View(list);
        }
        public ActionResult SearchResult(string key, string droplist)
        {
            if(droplist=="emp")
            {
               
                return RedirectToAction("SearchResultEmployee", "Search", new { key = key});
            }
            
            return RedirectToAction("SearchResultCustomer", "Search", new { key = key });
        }
        

    }
}