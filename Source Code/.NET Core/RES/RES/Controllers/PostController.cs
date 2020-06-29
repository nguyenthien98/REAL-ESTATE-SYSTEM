using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RES.Data.DBModels;
using RES.Models.Common;

namespace RES.Controllers
{
    public class PostController : Controller
    {
        private readonly RealEstateSystemContext _context;

        public PostController()
        {
            _context = new RealEstateSystemContext();
        }

        [Route("post/create")]
        public IActionResult Create()
        {
            string email = User.Identity.Name;
            AspNetUsers user = _context.AspNetUsers.Where(n => n.Email == email).Include(n => n.Customer).SingleOrDefault();

            if (user != null)
            {
                if (user.Customer.Count == 0)
                {
                    return LocalRedirect("~/input-information?returnUrl=~/post/create");
                }

                ViewData["Direction"] = new SelectList(_context.Direction, "DirectionId", "DirectionName");
                ViewData["PostType"] = new SelectList(_context.Type, "PostTypeId", "Name");
                ViewData["Project"] = new SelectList(_context.Project, "ProjectId", "ProjectName");
                ViewData["RealEstaleType"] = new SelectList(_context.RealEstateType, "RealEstateTypeId", "Name");

                return View();
            }
            else
            {
                return LocalRedirect("~/Identity/Account/Login?returnUrl=~/post/create");
            }
        }

        [Route("post/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,PostType,PostTime,Tittle,Price,Location,Area,Project,Description,RealEstaleType,Detail,AuthorId,AuthorEmpId,Status")] Post post,
            int bedroom = 0, int bathroom = 0, int floor = 0, bool alley = false, int direction = 1, List<IFormFile> files = null,
            string province = "", string district = "", string ward = "")
        {
            string email = User.Identity.Name;
            AspNetUsers user = _context.AspNetUsers.Where(n => n.Email == email).SingleOrDefault();

            if (user == null)
            {
                return LocalRedirect("~/Identity/Account/Login?returnUrl=~/post/create");
            }
            else
            {
                Customer customer = _context.Customer.Where(n => n.AccountId == user.Id).SingleOrDefault();

                if (customer == null)
                {
                    return LocalRedirect("~/input-information?returnUrl=~/post/create");
                }

                if (post.RealEstaleType == 1)
                {
                    post.DetailNavigation = new Detail()
                    {
                        Alley = alley,
                        Bathroom = bathroom,
                        Bedroom = bedroom,
                        DirectionId = direction,
                        Floor = floor,
                        ModifiedDate = DateTime.Now
                    };
                }

                post.Location += ", " + ward + ", " + district + ", " + province;
                post.PostId = CommonFunction.RemoveUnicode((post.Tittle + "-" + DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(".", "").Replace(" ", ""))).ToLower().Replace(" ", "-");
                post.AuthorId = customer.CustomerId;
                post.PostTime = DateTime.Now;

                if (files.Count > 0 && files[0].Length > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        var file = files[i];

                        if (file != null && files[i].Length > 0)
                        {
                            string fileName = Path.GetFileName(file.FileName);

                            string extensionFileName = Path.GetExtension(fileName);

                            fileName = fileName.Substring(0, fileName.Length - extensionFileName.Length) + "-" + DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "") + extensionFileName;

                            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\posts", fileName);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            PostImage pstImg = new PostImage();
                            pstImg.Url = fileName;
                            pstImg.AddedDate = DateTime.Now;
                            post.PostImage.Add(pstImg);
                        }
                    }

                }



                _context.Add(post);
                await _context.SaveChangesAsync();

                TempData["Notice"] = "Tạo tin đăng thành công. Tin đăng đang chờ duyệt bởi admin.";

                return RedirectToAction("Manager", "Post");
            }

            //ViewData["Direction"] = new SelectList(_context.Direction, "DirectionId", "DirectionName");
            //ViewData["PostType"] = new SelectList(_context.Type, "PostTypeId", "Name", post.PostType);
            //ViewData["Project"] = new SelectList(_context.Project, "ProjectId", "Location", post.Project);
            //ViewData["RealEstaleType"] = new SelectList(_context.RealEstateType, "RealEstateTypeId", "Name", post.RealEstaleType);

            //return View(post);
        }

        [Route("post/manager")]
        public IActionResult Manager()
        {
            string email = User.Identity.Name;
            AspNetUsers user = _context.AspNetUsers.Where(n => n.Email == email).SingleOrDefault();
            var lstPost = new List<Post>();

            if (user != null)
            {
                Customer customer = _context.Customer.Where(n => n.AccountId == user.Id).SingleOrDefault();

                if (customer == null)
                {
                    return RedirectToAction("Create", "Customer");
                }

                lstPost = _context.Post.Where(n => n.AuthorId == customer.CustomerId).OrderByDescending(n => n.PostTime)
                            .Include(n => n.RealEstaleTypeNavigation)
                            .Include(n => n.PostTypeNavigation)
                            .Include(n => n.Author)
                            .ToList();

                return View(lstPost);
            }
            else
            {
                return LocalRedirect("~/Identity/Account/Login?returnUrl=~/post/manager");
            }
        }

        [Route("canmua")]
        public IActionResult Index(int page = 1, int id = 0)
        {
            float pageCountF = _context.Post.Where(n => n.PostType == 1 && n.Status == 2).Count() / 12;

            int pageCountI = 1;

            if (pageCountF > (int)pageCountF)
            {
                pageCountI = (int)pageCountF + 1;
            }

            ViewBag.Title = "Cần mua | Bất động sản Miền Trung";
            ViewBag.Page = page;
            ViewBag.PageCount = pageCountI;

            List<Post> lstPosts = null;

            if (id == 0)
            {
                lstPosts = _context.Post.Where(n => n.PostType == 1 && n.Status == 2).OrderByDescending(n => n.PostTime).Skip(12 * (page - 1)).Take(12).ToList();
            }
            else
            {
                lstPosts = _context.Post.Where(n => n.PostType == 1 && n.Status == 2 && n.RealEstaleType == id).OrderByDescending(n => n.PostTime).Skip(12 * (page - 1)).Take(12).ToList();
            }
            return View("Index", lstPosts);
        }

        [Route("canthue")]
        public IActionResult Rents(int page = 1, int id = 0)
        {
            float pageCountF = _context.Post.Where(n => n.PostType != 1 && n.Status == 2).Count() / 12;

            int pageCountI = 1;

            if (pageCountF > (int)pageCountF)
            {
                pageCountI = (int)pageCountF + 1;
            }

            ViewBag.Title = "Cần thuê | Bất động sản Miền Trung";
            ViewBag.Page = page;
            ViewBag.PageCount = pageCountI;

            List<Post> lstPosts = null;

            if (id == 0)
            {
                lstPosts = _context.Post.Where(n => n.PostType != 1 && n.Status == 2).OrderByDescending(n => n.PostTime).Skip(12 * (page - 1)).Take(12).ToList();
            }
            else
            {
                lstPosts = _context.Post.Where(n => n.PostType != 1 && n.Status == 2 && n.RealEstaleType == id).OrderByDescending(n => n.PostTime).Skip(12 * (page - 1)).Take(12).ToList();
            }
            return View("Index", lstPosts);
        }

        [Route("real-estate-type")]
        public IActionResult SearchByRETypes(int page = 1, int id = 0)
        {
            float pageCountF = _context.Post.Where(n => n.PostType != 1 && n.Status == 2).Count() / 12;

            int pageCountI = 1;

            if (pageCountF > (int)pageCountF)
            {
                pageCountI = (int)pageCountF + 1;
            }

            ViewBag.Title = "Tìm theo loại bất động sản | Bất động sản Miền Trung";
            ViewBag.Page = page;
            ViewBag.PageCount = pageCountI;

            List<Post> lstPosts = null;

            if (id == 0)
            {
                lstPosts = _context.Post.Where(n => n.Status == 2).OrderByDescending(n => n.PostTime).Skip(12 * (page - 1)).Take(12).ToList();
            }
            else
            {
                lstPosts = _context.Post.Where(n => n.RealEstaleType == id && n.Status == 2 && n.RealEstaleType == id).OrderByDescending(n => n.PostTime).Skip(12 * (page - 1)).Take(12).ToList();
            }
            return View("Index", lstPosts);
        }

        [HttpGet("/{postId}")]
        public IActionResult PostDetail(string postId = "")
        {
            Post post = _context.Post.Include(n => n.Author).SingleOrDefault(n => n.PostId == postId);

            if (post == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("PostDetail", post);
        }

        [Route("search")]
        [HttpGet]
        public IActionResult Search(string keyWord = null, int type = 0, int priceType = 0, int page = 1)
        {
            List<Post> lstPosts = new List<Post>();

            long priceMin = 0;
            long priceMax = long.MaxValue;

            findMinMaxPrice(ref priceMin, ref priceMax, priceType);

            if (keyWord == null)
            {
                if (type == 0)
                {
                    if (priceType == 0)
                    {
                        lstPosts = _context.Post.Where(n => n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                    }
                    else
                    {
                        lstPosts = _context.Post.Where(n => n.Price >= priceMin && n.Price <= priceMax && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                    }
                }
                else
                {
                    if (priceType == 0)
                    {
                        if (type == 1)
                        {
                            lstPosts = _context.Post.Where(n => n.PostType == 1 && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                        }
                        else
                        {
                            lstPosts = _context.Post.Where(n => n.PostType != 1 && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                        }
                    }
                    else
                    {
                        if (type == 1)
                        {
                            lstPosts = _context.Post.Where(n => n.PostType == 1 && n.Price >= priceMin && n.Price <= priceMax && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                        }
                        else
                        {
                            lstPosts = _context.Post.Where(n => n.PostType != 1 && n.Price >= priceMin && n.Price <= priceMax && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                        }
                    }
                }
            }
            else
            {
                if (type == 0)
                {
                    if (priceType == 0)
                    {
                        lstPosts = _context.Post.Where(n => n.Location.Contains(keyWord) && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                    }
                    else
                    {
                        lstPosts = _context.Post.Where(n => n.Location.Contains(keyWord) && n.Price >= priceMin && n.Price <= priceMax && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                    }
                }
                else
                {
                    if (priceType == 0)
                    {
                        if (type == 1)
                        {
                            lstPosts = _context.Post.Where(n => n.Location.Contains(keyWord) && n.PostType == 1 && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                        }
                        else
                        {
                            lstPosts = _context.Post.Where(n => n.Location.Contains(keyWord) && n.PostType != 1 && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                        }
                    }
                    else
                    {
                        if (type == 1)
                        {
                            lstPosts = _context.Post.Where(n => n.Location.Contains(keyWord) && n.PostType == 1 && n.Price >= priceMin && n.Price <= priceMax && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                        }
                        else
                        {
                            lstPosts = _context.Post.Where(n => n.Location.Contains(keyWord) && n.PostType != 1 && n.Price >= priceMin && n.Price <= priceMax && n.Status == 2).OrderByDescending(n => n.PostTime).ToList();
                        }
                    }
                }
            }

            // Đếm số trang
            float pageCountF = lstPosts.Count / 12;

            int pageCountI = 1;

            if (pageCountF > (int)pageCountF)
            {
                pageCountI = (int)pageCountF + 1;
            }

            ViewBag.Title = "Tìm kiếm | Bất động sản Miền Trung";
            ViewBag.Page = page;
            ViewBag.PageCount = pageCountI;

            // Lấy 12 bài đăng theo trang từ lstPosts gán vào lstPostResult
            List<Post> lstPostResult = new List<Post>();

            int minRange = 12 * (page - 1) < lstPosts.Count ? 12 * (page - 1) : lstPosts.Count;
            int maxRange = 12 * (page - 1) + 12 < lstPosts.Count ? 12 * (page - 1) + 12 : lstPosts.Count;

            for (int i = minRange; i < maxRange; i++)
            {
                lstPostResult.Add(lstPosts[i]);
            }

            return View("Index", lstPostResult);
        }

        private void findMinMaxPrice(ref long minPrice, ref long maxPrice, int priceType)
        {
            switch (priceType)
            {
                case 1:
                    maxPrice = 499000000;
                    break;
                case 2:
                    minPrice = 500000000;
                    maxPrice = 800000000;
                    break;
                case 3:
                    minPrice = 800000000;
                    maxPrice = 1000000000;
                    break;
                case 4:
                    minPrice = 1000000000;
                    maxPrice = 2000000000;
                    break;
                case 5:
                    minPrice = 2000000000;
                    maxPrice = 3000000000;
                    break;
                case 6:
                    minPrice = 3000000000;
                    maxPrice = 5000000000;
                    break;
                case 7:
                    minPrice = 5000000000;
                    maxPrice = 7000000000;
                    break;
                case 8:
                    minPrice = 7000000000;
                    maxPrice = 10000000000;
                    break;
                case 9:
                    minPrice = 10000000000;
                    maxPrice = 20000000000;
                    break;
                case 10:
                    minPrice = 20000000000;
                    break;

                default:
                    break;
            }
        }
    }
}