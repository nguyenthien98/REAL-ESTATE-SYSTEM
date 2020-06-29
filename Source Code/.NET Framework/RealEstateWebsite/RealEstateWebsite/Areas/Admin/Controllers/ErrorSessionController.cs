using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateWebsite.Areas.Admin.Controllers
{
    public class ErrorSessionController : Controller
    {
        // GET: Admin/ErrorSession
        public ActionResult DefaultError()
        {
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
    }
}
