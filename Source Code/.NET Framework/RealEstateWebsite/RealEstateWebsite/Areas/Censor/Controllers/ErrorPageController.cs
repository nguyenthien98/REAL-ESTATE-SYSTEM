using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateWebsite.Areas.Censor.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: Censor/ErrorPage
        public ActionResult Error_404()
        {
            return View();
        }

        public ActionResult Error_500()
        {
            return View();
        }
    }
}