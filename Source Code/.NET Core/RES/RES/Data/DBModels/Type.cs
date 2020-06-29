using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Type
    {
        public Type()
        {
            Post = new HashSet<Post>();
        }

        public int PostTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}