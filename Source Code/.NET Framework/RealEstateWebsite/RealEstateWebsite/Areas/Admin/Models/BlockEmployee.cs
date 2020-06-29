using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstateWebsite.Models;
using System.Data.SqlClient;

namespace RealEstateWebsite.Areas.Admin.Models
{
    public class BlockEmployee
    {
        public static void BlockEmp(int idEmp, string Reason, DateTime? unBlockDate)
        {
            using (RealEstateWebsiteEntities db = new RealEstateWebsiteEntities())
            {
                if (unBlockDate == null)
                    unBlockDate = DateTime.Now.AddMonths(1);
                db.USP_InsertBlockEmp(idEmp, unBlockDate, Reason, DateTime.Now);
            }

        }
        public static void UnBlockEmp(int idEmp)
        {
            using (RealEstateWebsiteEntities db = new RealEstateWebsiteEntities())
            {
                db.USP_UnBlockBlockEmp(idEmp, DateTime.Now);
            }
        }
    }
}