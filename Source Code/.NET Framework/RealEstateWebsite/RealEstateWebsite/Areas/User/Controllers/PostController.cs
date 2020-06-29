using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;
namespace RealEstateWebsite.Areas.User.Controllers
{
    public class PostController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
        // GET: User/Post
        public PartialViewResult PostPartial()
        {
            List<Post> lstpst = db.Posts.Where(n => n.Status == 2).ToList();
            if (lstpst.Count == 0)
            {
                ViewBag.ThongBao = "No post was approved";
            }
            List<Post> lst_duyet = lstpst.OrderByDescending(n => n.PostTime).Take(6).ToList();
            return PartialView(lst_duyet);
        }
        public PartialViewResult PostThreePartial()
        {
            List<Post> lstpst = db.Posts.Where(n => n.Status == 2).ToList();
            if (lstpst.Count == 0)
            {
                ViewBag.ThongBao = "No post was approved";
            }
            List<Post> lst_duyet = lstpst.OrderByDescending(n => n.PostTime).Take(3).ToList();
            return PartialView(lst_duyet);
        }
        public PartialViewResult ReadmorePartial(int Post_id)
        {
            Post pst = db.Posts.SingleOrDefault(n => n.Post_ID == Post_id);
            if (pst == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return PartialView(pst);
        }
        public PartialViewResult ProjectPartial()
        {
            List<Project> lstprj = db.Projects.OrderByDescending(n => n.ModifiedDate).ToList();
            return PartialView(lstprj);
        }
    }
}