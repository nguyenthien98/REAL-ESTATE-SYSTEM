using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RES.Data.DBModels;

namespace RES.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/informations")]
    public class InformationController : Controller
    {
        private readonly RealEstateSystemContext _context;

        public InformationController()
        {
            _context = new RealEstateSystemContext();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var information = await _context.Information.FirstAsync();

            if (information == null)
            {
                return NotFound();
            }

            return View(information);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("InfoId,Email,Address,PhoneNumber,WorkingTime,Facebook,Twitter,Instagram,Pinterest,Linkedin")] Information information)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(information);
                    await _context.SaveChangesAsync();

                    TempData["Notice"] = "Lưu thông tin thành công";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationExists(information.InfoId))
                    {
                        return NotFound();
                    }
                }

            }

            TempData["Notice"] = "Lỗi: Không thể lưu thông tin, vui lòng thử lại sau.";
            return View(information);
        }

        private bool InformationExists(int id)
        {
            return _context.Information.Any(e => e.InfoId == id);
        }
    }
}
