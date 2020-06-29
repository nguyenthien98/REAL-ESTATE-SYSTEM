using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RES.Data.DBModels;

namespace RES.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/post")]
    public class PostController : Controller
    {
        RealEstateSystemContext _context = null;

        public PostController() { _context = new RealEstateSystemContext(); }

        [Route("approve")]
        public async Task<IActionResult> Approve(string id = "")
        {
            if (id == "")
            {
                return RedirectToAction("NewPost", "Home", new { Area = "Admin" });
            }

            PostStatus ps = new PostStatus()
            {
                PostId = id,
                Status = 2,
                CensorshipTime = DateTime.Now,
                Reason = "This post was approved by admin"

            };

            _context.Add(ps);
            await _context.SaveChangesAsync();

            TempData["Notice"] = "Duyệt tin thành công";

            return RedirectToAction("NewPost", "Home", new { Area = "Admin" });
        }

        [Route("delete")]
        public async Task<IActionResult> Delete(string id = "", string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (id == "")
            {
                return NotFound("Cannot found any post id" + id);
            }

            PostStatus ps = new PostStatus()
            {
                PostId = id,
                Status = 4,
                CensorshipTime = DateTime.Now,
                Reason = "This post was hided"
            };
            
            TempData["Notice"] = "Ẩn tin thành công";

            _context.Add(ps);
            await _context.SaveChangesAsync();

            return LocalRedirect(returnUrl);
        }
    }
}