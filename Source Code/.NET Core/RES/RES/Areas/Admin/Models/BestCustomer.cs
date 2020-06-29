using Microsoft.EntityFrameworkCore;
using RES.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RES.Areas.Admin.Models
{
    public class BestCustomer
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public int totalPosts { get; set; }
        public int totalSoldPosts { get; set; }
        public int totalSellingPosts { get; set; }

        private RealEstateSystemContext _context = new RealEstateSystemContext();

        public BestCustomer()
        {
            int max = _context.Customer.Max(n => n.Post.Count);
            Customer customer = _context.Customer.Where(n => n.Post.Count == max).Include(n => n.Post).First();

            this.id = customer.CustomerId;
            this.fullName = customer.LastName + " " + customer.Firstname;
            this.totalPosts = customer.Post.Count;
            this.totalSellingPosts = customer.Post.Where(n => n.Status == 2).Count();
            this.totalSoldPosts = customer.Post.Where(n => n.Status == 3 || n.Status == 4).Count();
        }
    }
}
