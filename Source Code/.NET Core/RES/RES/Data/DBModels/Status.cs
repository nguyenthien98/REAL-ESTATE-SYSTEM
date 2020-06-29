using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Status
    {
        public Status()
        {
            PostStatus = new HashSet<PostStatus>();
        }

        public int StatusId { get; set; }
        public string StatusType { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<PostStatus> PostStatus { get; set; }
    }
}