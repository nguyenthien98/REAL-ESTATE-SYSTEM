using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Information
    {
        public int InfoId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkingTime { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Pinterest { get; set; }
        public string Linkedin { get; set; }
    }
}