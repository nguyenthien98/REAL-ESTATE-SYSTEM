using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class PostImage
    {
        public int ImageId { get; set; }
        public string PostId { get; set; }
        public string Url { get; set; }
        public DateTime? AddedDate { get; set; }

        public virtual Post Post { get; set; }
    }
}