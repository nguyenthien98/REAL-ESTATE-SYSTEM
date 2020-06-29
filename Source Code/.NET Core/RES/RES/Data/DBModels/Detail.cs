using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Detail
    {
        public Detail()
        {
            Post = new HashSet<Post>();
        }

        public int DetailId { get; set; }
        public int? Floor { get; set; }
        public int? Bedroom { get; set; }
        public int? Bathroom { get; set; }
        public int? DirectionId { get; set; }
        public bool? Alley { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Direction Direction { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}