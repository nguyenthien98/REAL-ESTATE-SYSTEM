using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RES.Models;
using RES.Models.Data;

namespace RES.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Home_Index data = new Home_Index();

            PostModel postModel = new PostModel();

            data.lst4NewestPosts = postModel.get4NewestPosts();
            data.lst6PopularPosts = postModel.get6PopularPosts();

            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("gioithieu")]
        public IActionResult About()
        {
            return View();
        }

        [Route("lienhe")]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
