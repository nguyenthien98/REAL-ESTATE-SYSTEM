using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using RealEstateWebsite.Models;
using RealEstateWebsite.Areas.Censor.Models;

namespace RealEstateWebsite.Areas.Censor.Controllers
{

    public class PostController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
        // GET: Censor/Post

        public ViewResult Index()
        {
            var lstPost = db.Posts.ToList();

            List<Post> lstPostsResult = new List<Post>();
            foreach (var item in lstPost)
            {
                if (item.PostTime.ToString().Substring(0, 10) == DateTime.Now.ToString().Substring(0, 10))
                {
                    lstPostsResult.Add(item);
                }
            }
            return View(lstPostsResult);
        }

        public ViewResult Search(int typeOfPost, int typeOfRE, string date)
        {
            List<Post> lstPost;

            if (typeOfPost == 0 && typeOfRE == 0)
            {
                lstPost = db.Posts.ToList();
            }
            else
            {
                if (typeOfPost == 0)
                {
                    lstPost = db.Posts.Where(n => n.RealEstateType.RealEstateType_ID == typeOfRE).ToList();
                }
                else
                {
                    if(typeOfRE == 0)
                    {
                        lstPost = db.Posts.Where(n => n.Type1.PostType_ID == typeOfPost).ToList();
                    }
                    else
                    {
                        lstPost = db.Posts.Where(n => n.Type1.PostType_ID == typeOfPost && n.RealEstateType.RealEstateType_ID == typeOfRE).ToList();
                    }
                }
            }

            List<Post> lstPostsResult = new List<Post>();
            string dateConv = DateTime.ParseExact(date.Replace(".", "/"), "dd/MM/yyyy", null).ToString().Substring(0, 10);
            foreach (var item in lstPost)
            {
                if (item.PostTime.ToString().Substring(0, 10) == dateConv)
                {
                    lstPostsResult.Add(item);
                }
            }

            ViewBag.Date = date;
            ViewBag.TypeOfPost = typeOfPost;
            ViewBag.TypeOfRE = typeOfRE;

            return View("Index", lstPostsResult);
        }

        public ViewResult PendingPost()
        {
            List<Post> lstPost = db.Posts.Where(n => n.Status == 1).Take(10).ToList();
            return View(lstPost);
        }

        public ViewResult ReportPost()
        {
            List<Post_Report> lstPostReport = db.Post_Report.Where(n => n.Status == 0).ToList();
            return View(lstPostReport);
        }

        [HttpGet]
        public ViewResult AddPost()
        {
            PostData postData = new PostData();
            postData.lstDirection = db.Directions.ToList();
            postData.lstPostType = db.Type1.ToList();
            postData.lstProject = db.Projects.ToList();
            postData.lstRealEstateType = db.RealEstateTypes.ToList();

            if (TempData["addNewPost"] != null && TempData["addNewPost"].ToString() != "")
            {
                ViewBag.AddNewPost = TempData["addNewPost"].ToString();
            }

            return View(postData);
        }

        [HttpPost]
        public ActionResult AddPost(PostData postData, string posttype, string project, string typeOfRealEstate, string bedroom, string bathroom, string floor, string alley, string direction)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    Employee poster = Session["Account_Censor"] as Employee;

                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            var file = Request.Files[i];

                            if (file != null && Request.Files[i].ContentLength > 0)
                            {
                                string pic = Path.GetFileName(file.FileName);

                                string extensionFileName = CommonFunction.getExtensionFileName(pic);

                                pic = CommonFunction.hashSHA256(pic) + extensionFileName;

                                string path = Path.Combine(Server.MapPath(Constants.POST_IMG_URL), pic);

                                file.SaveAs(path);

                                Post_Image pstImg = new Post_Image();
                                pstImg.url = pic;
                                postData.post.Post_Image.Add(pstImg);
                            }
                        }

                    }

                    postData.post.Type1 = db.Type1.Single(n => n.PostType_ID.ToString() == posttype);
                    postData.post.Project = db.Projects.SingleOrDefault(n => n.Project_ID.ToString() == project);
                    postData.post.RealEstateType = db.RealEstateTypes.Single(n => n.RealEstateType_ID.ToString() == typeOfRealEstate);
                    postData.post.Employee = db.Employees.Find(poster.Employee_ID);

                    if (typeOfRealEstate == "1" && (bedroom != null || bathroom != null || floor != null || alley != null || direction != null))
                    {
                        Detail detail = new Detail();
                        detail.Bedroom = Convert.ToInt32(bedroom);
                        detail.Bathroom = Convert.ToInt32(bathroom);
                        detail.Floor = Convert.ToInt32(floor);
                        detail.Alley = alley == "1" ? true : false;
                        detail.Direction = db.Directions.SingleOrDefault(n => n.Direction_ID.ToString() == direction);

                        postData.post.Detail = detail;
                    }

                    db.Posts.Add(postData.post);

                    db.SaveChanges();

                    Post_Status ps = new Post_Status();
                    ps.Status = db.Status.Find(2);
                    ps.Employee = db.Employees.Find(poster.Employee_ID);
                    ps.Reason = "This post was created by Employee ID: " + poster.Employee_ID;

                    postData.post.Post_Status.Add(ps);
                    db.SaveChanges();

                    trans.Commit();

                    TempData["addNewPost"] = "OK";
                    return RedirectToAction("AddPost");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    trans.Rollback();
                    return View();
                }
            }
        }

        public ActionResult Details(string post_ID, string type)
        {
            Post post = db.Posts.FirstOrDefault(n => n.Post_ID.ToString() == post_ID);
            if (post == null)
            {
                return HttpNotFound();
            }

            ViewBag.Type = type;
            return PartialView("DetailsPartialView", post);
        }

        public string ApprovePost(string post_ID)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    Post post = db.Posts.SingleOrDefault(n => n.Post_ID.ToString() == post_ID);
                    Status stt = db.Status.Find(2);

                    if (post == null || stt == null)
                    {
                        RedirectToAction("Error_404", "ErrorPage");
                        return "0";
                    }

                    Post_Status ps = new Post_Status();
                    Employee censor = Session["Account_Censor"] as Employee;
                    ps.Employee = db.Employees.Find(censor.Employee_ID);
                    ps.Reason = "Approved Post";
                    ps.Post = post;
                    ps.Status = stt;

                    db.Post_Status.Add(ps);

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

        public string BlockPost(string post_ID)
        {
            try
            {
                Post post = db.Posts.Single(n => n.Post_ID.ToString() == post_ID);
                Status stt = db.Status.Find(4);

                if (post == null || stt == null)
                {
                    RedirectToAction("Error_404", "ErrorPage");
                    return "0";
                }

                Post_Status ps = new Post_Status();
                Employee censor = Session["Account_Censor"] as Employee;
                ps.Employee = db.Employees.Find(censor.Employee_ID);
                ps.Reason = "Blocked Post";
                ps.Post = post;
                ps.Status = stt;

                db.Post_Status.Add(ps);

                db.SaveChanges();



                return "1";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return "0";
            }
        }

        public ActionResult ReportDetails(string postReport_ID)
        {
            Post_Report post = db.Post_Report.SingleOrDefault(n => n.Post_Report_ID.ToString() == postReport_ID);
            if (post == null)
            {
                return HttpNotFound();
            }
            return PartialView("ReportPartialView", post);
        }

        public string ConfirmBlockReport(string postReport_ID)
        {

            try
            {
                Post_Report ps = db.Post_Report.Single(n => n.Post_Report_ID.ToString() == postReport_ID);
                Employee censor = Session["Account_Censor"] as Employee;
                censor = db.Employees.Find(censor.Employee_ID);

                // Add status for post
                Post post = db.Posts.Single(n => n.Post_ID.ToString() == ps.Post.Post_ID.ToString());
                Status stt = db.Status.Find(4);

                if (post == null || stt == null)
                {
                    RedirectToAction("Error_404", "ErrorPage");
                    return "0";
                }

                Post_Status pst = new Post_Status();

                pst.Employee = censor;
                pst.Reason = "Blocked Post";
                pst.Post = post;
                pst.Status = stt;

                db.Post_Status.Add(pst);
                // End add status for post


                // Edit Post_Report
                ps.Status = 1;

                ps.Employee = censor;

                db.SaveChanges();



                return "1";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return "0";
            }

        }

        public string DeleteBlockReport(string postReport_ID)
        {

            try
            {
                Post_Report ps = db.Post_Report.Single(n => n.Post_Report_ID.ToString() == postReport_ID);

                Employee emp = Session["Account_Censor"] as Employee;
                ps.Employee = db.Employees.Find(emp.Employee_ID);

                ps.Status = 1;

                db.SaveChanges();



                return "1";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return "0";
            }

        }
    }
}