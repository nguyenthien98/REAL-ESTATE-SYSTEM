using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class PostStatus
    {
        public int PostStatusId { get; set; }
        public string PostId { get; set; }
        public int Status { get; set; }
        public DateTime? CensorshipTime { get; set; }
        public string Reason { get; set; }

        public virtual Post Post { get; set; }
        public virtual Status StatusNavigation { get; set; }
    }
}