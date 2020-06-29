using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;
namespace RealEstateWebsite.Areas.User.Controllers
{
    public class TypePostController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
        // GET: User/TypePost
        public PartialViewResult NeedToBuyPartial()
        {
            List<Post> lstpstbuy = db.Posts.Where(n => n.Type1.PostType_ID==1).ToList();
            if (lstpstbuy.Count == 0)
            {
                ViewBag.Error = "There are no post";
            }
            return PartialView(lstpstbuy);
        }
        public PartialViewResult NeedToRentPartial()
        {
            List<Post> lstpstbuy = db.Posts.Where(n => n.Type1.PostType_ID == 2).ToList();
            if (lstpstbuy.Count == 0)
            {
                ViewBag.Error = "There are no post";
            }
            return PartialView(lstpstbuy);
        }
    }
}