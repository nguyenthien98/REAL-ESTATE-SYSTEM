using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;
namespace RealEstateWebsite.Areas.User.Controllers
{
    public class NeedToBuyController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
        // GET: User/NeedToBuy
        public PartialViewResult HousePartial()
        {
            List<Post> lstpstbuy = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 1 && n.Type1.PostType_ID == 1).ToList();
            if (lstpstbuy.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstbuy);
        }
        public PartialViewResult LandPartial() // đất
        {
            List<Post> lstpstbuy = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 2 && n.Type1.PostType_ID == 1).ToList();
            if (lstpstbuy.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstbuy);
        }
        public PartialViewResult ApartmentPartial() // căn hộ
        {
            List<Post> lstpstbuy = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 3 && n.Type1.PostType_ID == 1).ToList();
            if (lstpstbuy.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstbuy);
        }
        public PartialViewResult PremisesPartial() // mặt bằng
        {
            List<Post> lstpstbuy = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 4 && n.Type1.PostType_ID == 1).ToList();
            if (lstpstbuy.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstbuy);
        }
        public PartialViewResult WareHousePartial() // kho xưởng
        {
            List<Post> lstpstbuy = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 5 && n.Type1.PostType_ID == 1).ToList();
            if (lstpstbuy.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstbuy);
        }
        public PartialViewResult OtherPartial()
        {
            List<Post> lstpstbuy = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 10 && n.Type1.PostType_ID == 1).ToList();
            if (lstpstbuy == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return PartialView(lstpstbuy);
        }
    }
}