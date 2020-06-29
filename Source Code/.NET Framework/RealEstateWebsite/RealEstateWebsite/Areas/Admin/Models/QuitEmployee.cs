using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstateWebsite.Models;

namespace RealEstateWebsite.Areas.Admin.Models
{
    public class QuitEmployee
    {
        public static void QuitEmp(int idEmp, string Reason)
        {
            using (RealEstateWebsiteEntities db = new RealEstateWebsiteEntities())
            {
                db.USP_InsertQuitEmp(idEmp, Reason, DateTime.Now);
            }
        }
    }
}