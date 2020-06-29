using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class SubMenu
    {
        public int SubId { get; set; }
        public int? MenuId { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }

        public virtual Menu Menu { get; set; }
    }
}