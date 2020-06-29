using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RES.Data.DBModels;

namespace RES.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RealEstateSystemContext _context;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = new RealEstateSystemContext();
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Firstname { get; set; }
            [Required]
            public string LastName { get; set; }
            public string Email { get; set; }
            [Required]
            [MinLength(10)]
            public string Address { get; set; }
            [Required]
            [Phone]
            public string PhoneNumber { get; set; }
            public string AvatarUrl { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Customer customer = _context.Customer.Where(n => n.AccountId == user.Id).SingleOrDefault() ?? new Customer();

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);

            Input = new InputModel
            {
                Firstname = customer.Firstname,
                LastName = customer.LastName,
                Email = user.Email,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber,
                AvatarUrl = customer.AvatarUrl
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            var customer = _context.Customer.Where(n => n.AccountId == user.Id).SingleOrDefault() ?? new Customer();

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            customer.Firstname = Input.Firstname;
            customer.LastName = Input.LastName;
            customer.Address = Input.Address;
            customer.PhoneNumber = Input.PhoneNumber;

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

            if (customer.CustomerId == 0)    // customer mới
            {
                if (customer.AvatarUrl == null || customer.AvatarUrl == "")
                {
                    customer.AvatarUrl = "noAvatar.png";
                }

                customer.AccountId = user.Id;
                customer.Email = user.UserName;
                customer.CreatedDate = DateTime.Now;

                _context.Customer.Add(customer);
            }

            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("User_Name_Session", customer.LastName + " " + customer.Firstname);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Thông tin đã cập nhật thành công";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
