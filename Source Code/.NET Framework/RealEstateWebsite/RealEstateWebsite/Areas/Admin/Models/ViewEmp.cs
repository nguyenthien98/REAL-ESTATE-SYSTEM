using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstateWebsite.Models;
namespace RealEstateWebsite.Areas.Admin.Models
{
    public class ViewEmp
    {
        public static List<Employee> GetListEmp()
        {
            using (RealEstateWebsiteEntities db = new RealEstateWebsiteEntities())
            {
                return db.Employees.ToList();
            }
        }
        public static Employee GetOneEmp(int id)
        {
            using (RealEstateWebsiteEntities db = new RealEstateWebsiteEntities())
            {
                return db.Employees.Where(p=>p.Employee_ID==id).SingleOrDefault();
            }
        }
        public static void UpdateEmp(Employee model)
        {
            using (RealEstateWebsiteEntities db = new RealEstateWebsiteEntities())
            {
                db.Employees.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}