using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Direction
    {
        public Direction()
        {
            Detail = new HashSet<Detail>();
        }

        public int DirectionId { get; set; }
        public string DirectionName { get; set; }

        public virtual ICollection<Detail> Detail { get; set; }
    }
}