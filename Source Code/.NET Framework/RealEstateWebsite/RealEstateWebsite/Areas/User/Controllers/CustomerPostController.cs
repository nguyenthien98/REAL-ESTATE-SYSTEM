using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;
using System.IO;
using RealEstateWebsite.Areas.User.Models;

namespace RealEstateWebsite.Areas.User.Controllers
{
    public class CustomerPostController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
        [HttpGet]
        public ActionResult PostCustomer()
        {
            ViewBag.RealEstaleType = new SelectList(db.RealEstateTypes.ToList().OrderBy(n => n.Name), "RealEstateType_ID", "Name"); // lấy mã , hiển thị tên
            ViewBag.PostType = new SelectList(db.Type1.ToList().OrderBy(n => n.Name), "PostType_ID", "Name");
            ViewBag.Direction = new SelectList(db.Directions.ToList().OrderBy(n => n.Direction_Name), "Direction_ID", "Direction_Name");
            return View();
        }
        
        // GET: User/CustomerPost
        [HttpPost]
        public ActionResult PostCustomer(CustomerPost post, HttpPostedFileBase fileUpload)
        {
            ViewBag.RealEstaleType = new SelectList(db.RealEstateTypes.ToList().OrderBy(n => n.Name), "RealEstateType_ID", "Name");
            ViewBag.PostType = new SelectList(db.Type1.ToList().OrderBy(n => n.Name), "PostType_ID", "Name");
            ViewBag.Direction = new SelectList(db.Directions.ToList().OrderBy(n => n.Direction_Name), "Direction_ID", "Direction_Name");
            if (Session["Account"] == null || Session["Account"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                Account cst = Session["Account"] as Account;
                var account = db.Accounts.Where(x => x.Account_ID == cst.Account_ID);
                Project project = new Project();
                project.ProjectName = post.ProjectName;
                project.Location = post.LocationProject;
                project.Protential = post.Protential;

                Detail pst_detail = new Detail();
                pst_detail.Floor = post.Floor;
                pst_detail.Bedroom = post.Bedroom;
                pst_detail.Bathroom = post.Bathroom;
                pst_detail.Direction = db.Directions.Find(post.Direction);

                Post pst = new Post();
                var cust= db.Customers.Where(x => x.Account.Account_ID==cst.Account_ID).FirstOrDefault();
                pst.Customer = cust;
                pst.Tittle = post.Tittle;
                pst.Price = post.Price;
                pst.Location = post.Location;
                pst.Area = post.Area;
                pst.Description = post.Description;
                pst.RealEstateType = db.RealEstateTypes.Find(post.RealEstaleType);
                pst.Type1 = db.Type1.Find(post.PostType);
                Post_Image pst_img = new Post_Image();
                pst.Project = project;
                pst.Detail = pst_detail;
                //Lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);
                //Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/Images/Post"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Images already exists";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                pst_img.url = fileUpload.FileName;

                Project_Image pr_img = new Project_Image();
                var fileName1 = Path.GetFileName(fileUpload.FileName);
                var path1 = Path.Combine(Server.MapPath("~/Images/Project"), fileName1);
                if(System.IO.File.Exists(path1))
                {
                    ViewBag.ThongBao = "Images already exists";
                }
                else
                {
                    fileUpload.SaveAs(path1);
                }
                pr_img.url = fileUpload.FileName;

                db.Projects.Add(project);
                db.Posts.Add(pst);
                pst.Post_Image.Add(pst_img);
                project.Project_Image.Add(pr_img);
                db.Details.Add(pst_detail);
                db.SaveChanges();
                ViewBag.ThongBao1 = "Post successful";
            }
            return View();
        }
    }
}