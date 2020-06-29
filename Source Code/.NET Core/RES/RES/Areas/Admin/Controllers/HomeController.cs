using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RES.Areas.Admin.Models;
using RES.Data.DBModels;
using RES.Models.Security;

namespace RES.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class HomeController : Controller
    {
        RealEstateSystemContext _context = null;

        public HomeController()
        {
            _context = new RealEstateSystemContext();
        }

        public IActionResult Index()
        {
            HomeDataModel data = new HomeDataModel();
            return View(data);
        }

        [Route("new-post")]
        public IActionResult NewPost()
        {
            List<Post> lstPost = _context.Post.Where(n => n.Status == 1).OrderByDescending(n => n.PostTime)
                                    .Include(n => n.PostTypeNavigation)
                                    .Include(n => n.RealEstaleTypeNavigation)
                                    .Include(n => n.Author)
                                    .ToList();
            return View(lstPost);
        }

        [Route("search")]
        public IActionResult Search()
        {
            List<Post> lstPost = _context.Post.OrderByDescending(n => n.PostTime)
                                    .Include(n => n.PostTypeNavigation)
                                    .Include(n => n.RealEstaleTypeNavigation)
                                    .Include(n => n.Author)
                                    .ToList();
            return View(lstPost);
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("AdminSession");
            }
            catch (Exception)
            {
            }

            // Info.
            return LocalRedirect("~/Identity/Account/Login");
        }

        [Route("change-pasword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("change-pasword")]
        [HttpPost]
        public IActionResult ChangePassword(string password = "", string repassword = "?")
        {
            if(password != repassword)
            {
                TempData["Notice"] = "Lỗi: Mật khẩu nhập không khớp";
                return RedirectToAction("ChangePassword");
            }
            
            string username = HttpContext.Session.GetString("AdminSession");

            if (username == null) { return LocalRedirect("~/Identity/Account/Login"); }

            RES.Data.DBModels.Admin admin = _context.Admin.Where(n => n.UserName == username).SingleOrDefault();

            if (admin == null) { return NotFound(); }

            admin.PasswordHash = HashPwdTool.GeneratePassword(password);

            _context.SaveChangesAsync();

            TempData["Notice"] = "Đổi mật khẩu thành công";
            return RedirectToAction("ChangePassword");
        }
    }
}