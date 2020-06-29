using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class RealEstateType
    {
        public RealEstateType()
        {
            Post = new HashSet<Post>();
        }

        public int RealEstateTypeId { get; set; }
        public string Name { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}