using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Project
    {
        public Project()
        {
            Post = new HashSet<Post>();
        }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Location { get; set; }
        public string Protential { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}