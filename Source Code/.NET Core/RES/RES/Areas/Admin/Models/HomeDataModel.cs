using Microsoft.EntityFrameworkCore;
using RES.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RES.Areas.Admin.Models
{
    public class HomeDataModel
    {
        public int totalGuest { get; set; }
        public int totalNewUser { get; set; }
        public int totalUser { get; set; }
        public int totalPost { get; set; }
        public int totalBlockedUser { get; set; }
        public int totalSellingPost { get; set; }
        public int totalFBClick { get; set; }
        public int totalInsClick { get; set; }
        public int totalTwiClick { get; set; }
        public int totalLinClick { get; set; }
        public BestCustomer customer { get; set; }
        public ChartModel chart { get; set; }

        private RealEstateSystemContext _context = null;

        public HomeDataModel()
        {
            _context = new RealEstateSystemContext();
            Dashboard ds = _context.Dashboard.First();

            this.totalGuest = ds.TotalGuest ?? 0;
            this.totalNewUser = _context.Customer.Where(n => n.CreatedDate.Value.Month == DateTime.Now.Month).Count();
            this.totalUser = _context.Customer.Count();
            this.totalPost = _context.Post.Count();

            List<Customer> customers = _context.Customer.Where(n => n.Block.Count() != 0).Include(n => n.Block).ToList();
            customers = customers.Where(n => n.Block.Last().UnBlockDate.Equals(null)).ToList();
            this.totalBlockedUser = customers.Count;

            this.totalSellingPost = _context.Post.Where(n => n.Status == 2).Count();
            this.totalFBClick = ds.TotalFbclick ?? 0;
            this.totalInsClick = ds.TotalInsClick ?? 0;
            this.totalTwiClick = ds.TotalTwiClick ?? 0;
            this.totalLinClick = ds.TotalLinClick ?? 0;
            this.customer = new BestCustomer();
            this.chart = new ChartModel();
        }
    }
}
