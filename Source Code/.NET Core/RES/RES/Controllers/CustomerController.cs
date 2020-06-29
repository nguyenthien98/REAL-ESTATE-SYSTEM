using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RES.Data.DBModels;

namespace RES.Controllers
{
    public class CustomerController : Controller
    {
        private readonly RealEstateSystemContext _context;
        private string returnUrl = null;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CustomerController(SignInManager<IdentityUser> signInManager)
        {
            _context = new RealEstateSystemContext();
            _signInManager = signInManager;
        }

        [Route("input-information")]
        public IActionResult Create(string returnURL)
        {
            returnUrl = returnURL ?? Url.Content("~/");
            return View();
        }

        [Route("input-information")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Firstname,LastName,Email,Address,PhoneNumber,AccountId,AvatarUrl,ModifiedDate")] Customer customer, IFormFile file = null)
        {
            if (ModelState.IsValid)
            {
                returnUrl = returnUrl ?? Url.Content("~/");

                var email = User.Identity.Name;
                AspNetUsers user = _context.AspNetUsers.Where(n => n.Email == email).Single();

                customer.Email = email;
                customer.AccountId = user.Id;
                customer.ModifiedDate = DateTime.Now;
                customer.CreatedDate = DateTime.Now;

                if (file != null && file.Length > 0)
                {
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
                else
                {
                    customer.AvatarUrl = "noAvatar.png";
                }

                _context.Add(customer);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("User_Name_Session", customer.LastName + " " + customer.Firstname);

                return LocalRedirect(returnUrl);
            }

            return View(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
