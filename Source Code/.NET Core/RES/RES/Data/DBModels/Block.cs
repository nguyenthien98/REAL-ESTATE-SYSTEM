using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Block
    {
        public int CusId { get; set; }
        public int BlockId { get; set; }
        public string Reason { get; set; }
        public DateTime? BlockDate { get; set; }
        public DateTime? UnBlockDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Customer Cus { get; set; }
    }
}