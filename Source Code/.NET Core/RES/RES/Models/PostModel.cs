using RES.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RES.Models
{
    public class PostModel
    {
        private RealEstateSystemContext db;

        public PostModel()
        {
            db = new RealEstateSystemContext();
        }

        public PostModel(RealEstateSystemContext db)
        {
            this.db = db;
        }
        
        public List<Post> get4NewestPosts()
        {
            List<Post> lst3NewestPosts = db.Post.Where(n => n.Status == 2).OrderByDescending(n => n.PostTime).Take(4).ToList();

            return lst3NewestPosts;
        }

        public List<Post> get6PopularPosts()
        {
            List<Post> lst6PopularPosts = db.Post.Where(n => n.Status == 2 && n.RealEstaleType == 1 && n.Price < 4000000000 && n.Price > 800000000).OrderByDescending(n => n.PostTime).Take(6).ToList();

            return lst6PopularPosts;
        }
    }
}
