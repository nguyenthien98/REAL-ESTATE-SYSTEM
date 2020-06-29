using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstateWebsite.Models;
using RealEstateWebsite.Areas.Admin.Models;
namespace RealEstateWebsite.Areas.Admin.Models
{
    public class IndexAdmin
    {
        public IndexAdmin()
        {

        }
        public static Dashboard GetItem()
        {
            Dashboard dash = new Dashboard();
            RealEstateWebsiteEntities db = new RealEstateWebsiteEntities();
            {
                dash.Count_Employee = db.Employees.Count();
                dash.Count_Customer = db.Customers.Count();
                dash.Count_Post = db.Posts.Count();
                dash.Sum_Sale = db.Posts.Where(p=>p.PostTime.Value== DateTime.Now).Count();

            }
            return dash;
        }
    }
}