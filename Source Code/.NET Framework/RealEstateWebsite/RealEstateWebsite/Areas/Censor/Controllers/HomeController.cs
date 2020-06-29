using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateWebsite.Models;
using RealEstateWebsite.Areas.Censor.Models;

namespace RealEstateWebsite.Areas.Censor.Controllers
{
    public class HomeController : Controller
    {
        RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();

        // GET: Censor/Home
        public ActionResult Index()
        {
            DashboardData dashB = new DashboardData();

            dashB.totalPostsToday = db.Posts.Where(n => n.PostTime.ToString().Substring(0, 10) == DateTime.Now.ToString().Substring(0, 10)).ToList().Count;
            dashB.totalPendingPosts = db.Posts.Where(n => n.Status == 1).ToList().Count;
            dashB.totalReportedPosts = db.Post_Report.Where(n => n.Status == 0).ToList().Count;
            dashB.totalReportedCustomers = db.Customer_Report.Where(n => n.Status == 0).ToList().Count;
            dashB.chart = new List<DashboardData.ChartData>();

            // DateTime startDayOfWeek = CommonFunction.FirstDateInWeek(DateTime.Now, DayOfWeek.Monday);

            for (int i = 0; i < 7; i++)
            {
                //DateTime date = startDayOfWeek.AddDays(i);

                DashboardData.ChartData chartData = new DashboardData.ChartData();
                chartData.period = DateTime.Now.AddYears(-6).AddYears(i).Year.ToString();
                chartData.posts = db.Posts.Where(n => n.PostTime.Value.Year.ToString() == chartData.period).ToList().Count;
                chartData.usersOnline = db.Accounts.Where(n => n.Role_Account.FirstOrDefault().Role.Role_Name == "Customer" && n.AccountLogs.FirstOrDefault().ModifiedDate.Value.Year.ToString() == chartData.period).Select(n => n.Account_ID).Distinct().ToList().Count;
                dashB.chart.Add(chartData);
            }

            return View(dashB);
        }
    }
}