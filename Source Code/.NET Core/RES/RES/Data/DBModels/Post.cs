using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Post
    {
        public Post()
        {
            PostImage = new HashSet<PostImage>();
            PostReport = new HashSet<PostReport>();
            PostStatus = new HashSet<PostStatus>();
        }

        public string PostId { get; set; }
        public int PostType { get; set; }
        public DateTime? PostTime { get; set; }
        public string Tittle { get; set; }
        public long Price { get; set; }
        public string Location { get; set; }
        public decimal Area { get; set; }
        public int? Project { get; set; }
        public string Description { get; set; }
        public int RealEstaleType { get; set; }
        public int? Detail { get; set; }
        public int? AuthorId { get; set; }
        public int? Status { get; set; }

        public virtual Customer Author { get; set; }
        public virtual Detail DetailNavigation { get; set; }
        public virtual Type PostTypeNavigation { get; set; }
        public virtual Project ProjectNavigation { get; set; }
        public virtual RealEstateType RealEstaleTypeNavigation { get; set; }
        public virtual ICollection<PostImage> PostImage { get; set; }
        public virtual ICollection<PostReport> PostReport { get; set; }
        public virtual ICollection<PostStatus> PostStatus { get; set; }
    }
}