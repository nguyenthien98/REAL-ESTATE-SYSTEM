using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;
namespace RealEstateWebsite.Areas.User.Controllers
{
    public class ProjectController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
        // GET: User/Project
        public PartialViewResult AllProjectPartial()
        {
            List<Project> project = db.Projects.ToList();
            if(project.Count==0)
            {
                ViewBag.ThongBao = "Not find project";
            }
            return PartialView(project);
        }
        public PartialViewResult PostOfProject(int project_id)
        {
            List<Post> postofproject = db.Posts.Where(n => n.Project.Project_ID == project_id).ToList();
            if(postofproject.Count==0)
            {
                ViewBag.ThongBao = "There are no posts with project";
            }
            return PartialView(postofproject);
        }
    }
}