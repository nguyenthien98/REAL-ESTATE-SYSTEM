using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RES.Data.DBModels;
using RES.Models.Common;
using RES.Models.Security;

namespace RES.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/admin-manager")]
    public class AdminController : Controller
    {
        private readonly RealEstateSystemContext _context;

        public AdminController()
        {
            _context = new RealEstateSystemContext();
        }

        public async Task<IActionResult> Index()
        {
            string isMe = HttpContext.Session.GetString("AdminSession");
            var lstAdmin = await _context.Admin.Where(n => n.UserName != isMe).ToListAsync();

            return View(lstAdmin);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,UserName,PasswordHash")] RES.Data.DBModels.Admin admin)
        {
            if (ModelState.IsValid)
            {
                var adminDB = _context.Admin.Where(n => n.UserName == admin.UserName).SingleOrDefault();
                if(adminDB != null)
                {
                    TempData["Notice"] = "Lỗi: Tên tài khoản " + admin.UserName + " đã tồn tại.";
                    return RedirectToAction("Create");
                }
                admin.PasswordHash = HashPwdTool.GeneratePassword(admin.PasswordHash);
                _context.Add(admin);
                await _context.SaveChangesAsync();

                TempData["Notice"] = "Tạo admin " + admin.UserName + " thành công.";
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admin.FindAsync(id);
            _context.Admin.Remove(admin);
            await _context.SaveChangesAsync();

            TempData["Notice"] = "Xóa admin " + admin.UserName + " thành công.";
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admin.Any(e => e.AdminId == id);
        }
    }
}
