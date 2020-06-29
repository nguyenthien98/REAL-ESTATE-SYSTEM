using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Menu
    {
        public Menu()
        {
            SubMenu = new HashSet<SubMenu>();
        }

        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }

        public virtual ICollection<SubMenu> SubMenu { get; set; }
    }
}