using RES.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RES.Models.Data
{
    public class Home_Index
    {
        public List<Post> lst4NewestPosts { get; set; }
        public List<Post> lst6PopularPosts { get; set; }
        //public List<Blog> lst3NewestBlog { get; set; }

        public Home_Index()
        {
            using(RealEstateSystemContext db = new RealEstateSystemContext())
            {
                try
                {
                    Dashboard ds = db.Dashboard.First();
                    ds.TotalGuest += 1;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    
                }
            }
        }
    }
}
