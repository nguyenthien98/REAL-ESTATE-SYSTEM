using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RES.Data.DBModels;

namespace RES.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/customers")]
    public class CustomerController : Controller
    {
        private readonly RealEstateSystemContext _context;

        public CustomerController()
        {
            _context = new RealEstateSystemContext();
        }

        // GET: Admin/Customer
        public async Task<IActionResult> Index()
        {
            var realEstateSystemContext = _context.Customer.Include(c => c.Account).Include(c => c.Block);
            return View(await realEstateSystemContext.ToListAsync());
        }

        [HttpGet("{id}")]
        // GET: Admin/Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.Account)
                .Include(c => c.Post)
                .Include(c => c.Block)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpGet("edit/{id}")]
        // GET: Admin/Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/Customer/Edit/5
        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Firstname,LastName,Email,Address,PhoneNumber,AccountId,AvatarUrl,ModifiedDate")] Customer customer,
            IFormFile file = null)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        // Delete old image file
                        var oldPath = Directory.GetCurrentDirectory() + @"\wwwroot\images\avatars\" + customer.AvatarUrl;

                        if (System.IO.File.Exists(oldPath) && customer.AvatarUrl != "noAvatar.png")
                        {
                            System.IO.File.Delete(oldPath);
                        }

                        // Add new image file
                        string fileName = Path.GetFileName(file.FileName);

                        string extensionFileName = Path.GetExtension(fileName);

                        fileName = fileName.Substring(0, fileName.Length - extensionFileName.Length) + "-" + DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "") + extensionFileName;

                        var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\avatars", fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        customer.AvatarUrl = fileName;
                    }

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        TempData["Notice"] = "Lỗi: Không thể lưu thông tin, vui lòng thử lại sau.";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["Notice"] = "Lưu thông tin của " + customer.LastName + " " + customer.Firstname + " thành công";
                return RedirectToAction(nameof(Index));
            }
            
            return View(customer);
        }

        // POST: Admin/Customer/Block/5
        [HttpPost]
        [Route("block/{id}")]
        public async Task<IActionResult> Block(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Block block = new Block()
                {
                    CusId = id ?? 0,
                    UnBlockDate = null,
                    Reason = "Tài khoản bị khóa bởi admin"
                };

                _context.Block.Add(block);
                await _context.SaveChangesAsync();

                TempData["Notice"] = "Khóa tài khoản thành công.";
            }
            catch (Exception)
            {
                TempData["Notice"] = "Lỗi: Khóa tài khoản thất bại, vui lòng thử lại.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("unblock/{id}")]
        public async Task<IActionResult> UnBlock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Block block = await _context.Block.Where(n => n.CusId == id).LastOrDefaultAsync();

                if (block == null)
                {
                    return NotFound();
                }

                block.UnBlockDate = DateTime.Now;
                await _context.SaveChangesAsync();

                TempData["Notice"] = "Mở khóa tài khoản thành công.";
            }
            catch (Exception)
            {
                TempData["Notice"] = "Lỗi:  Mở khóa tài khoản thất bại, vui lòng thử lại.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
