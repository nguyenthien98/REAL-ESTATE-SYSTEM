using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;
using RealEstateWebsite.Areas.Admin.Models;
using Newtonsoft.Json;

namespace RealEstateWebsite.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home

        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HandleError]
        public ActionResult Index()
        {
            Dashboard model = IndexAdmin.GetItem();
            List<DataPoint> dataPoints = new List<DataPoint>();
           var listpost= db.USP_GetPostCountBydate();
            foreach(USP_GetPostCountBydate_Result a in listpost)
            {
                dataPoints.Add(new DataPoint(String.Format("{0:d}", a.date), Convert.ToDouble( a.count)));
            }
           

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View(model);
        }
    }
}