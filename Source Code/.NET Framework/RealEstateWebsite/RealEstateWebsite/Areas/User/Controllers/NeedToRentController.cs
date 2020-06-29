using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;

namespace RealEstateWebsite.Areas.User.Controllers
{
    public class NeedToRentController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
        // GET: User/NeedToRent
        public PartialViewResult HousePartial()
        {
            List<Post> lstpstrent = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 1 && n.Type1.PostType_ID == 2).ToList();
            if (lstpstrent.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstrent);
        }
        public PartialViewResult LandPartial() // đất
        {
            List<Post> lstpstrent = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 2 && n.Type1.PostType_ID == 2).ToList();
            if (lstpstrent.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstrent);
        }
        public PartialViewResult ApartmentPartial() // căn hộ
        {
            List<Post> lstpstrent = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 3 && n.Type1.PostType_ID == 2).ToList();
            if (lstpstrent.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstrent);
        }
        public PartialViewResult PremisesPartial() // mặt bằng
        {
            List<Post> lstpstrent = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 4 && n.Type1.PostType_ID == 2).ToList();
            if (lstpstrent.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstrent);
        }
        public PartialViewResult WareHousePartial() // kho xưởng
        {
            List<Post> lstpstrent = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 5 && n.Type1.PostType_ID == 2).ToList();
            if (lstpstrent.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstrent);
        }
        public PartialViewResult OtherPartial()
        {
            List<Post> lstpstrent = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == 10 && n.Type1.PostType_ID == 2).ToList();
            if (lstpstrent.Count == 0)
            {
                ViewBag.Error = "There are no posts";
            }
            return PartialView(lstpstrent);
        }
    }
}