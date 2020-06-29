using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstateWebsite.Models;


namespace RealEstateWebsite.Areas.Admin.Models
{
    public class BlockCustomer
    {
        public static void BlockCust(int idCust, string Reason, int idEmp, DateTime? unBlockDate)
        {
            using (RealEstateWebsiteEntities db = new RealEstateWebsiteEntities())
            {
                if (unBlockDate == null)
                {
                    unBlockDate = DateTime.Now.AddMonths(1);
                    db.USP_InsertBlockCust(idCust, DateTime.Now, unBlockDate, Reason, idEmp, DateTime.Now);
                }
                else
                {
                    db.USP_InsertBlockCust(idCust, DateTime.Now, unBlockDate, Reason, idEmp, DateTime.Now);
                }
            }

        }
        public static void UnBlockCust(int idCust)
        {
            using (RealEstateWebsiteEntities db = new RealEstateWebsiteEntities())
            {
                db.USP_UnBlockBlockCust(idCust, DateTime.Now, DateTime.Now);
            }
        }
    }
}