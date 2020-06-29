using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;
namespace RealEstateWebsite.Areas.User.Controllers
{
    public class SearchController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
        // GET: User/Search
        [HttpGet]

        public PartialViewResult ResultSearchPartial()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult ResultSearchPartial(string Key = "", decimal PriceNho = 0, decimal PriceLon = 999999999999)
        {   
            List<spSearchKeyPrice_Result> lstprice = db.spSearchKeyPrice(Key, PriceNho, PriceLon).ToList();
            if (lstprice.Count == 0)
            {
                ViewBag.ThongBao = "Post not found";
            }
            ViewBag.ThongBao = "Found " + lstprice.Count + " post";
            return PartialView("ResultSearchPartial", lstprice);
        }
    }
}