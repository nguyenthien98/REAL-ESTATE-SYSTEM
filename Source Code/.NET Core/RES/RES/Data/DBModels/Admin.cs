using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}