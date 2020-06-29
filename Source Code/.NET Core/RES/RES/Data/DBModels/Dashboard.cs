using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Dashboard
    {
        public string Id { get; set; }
        public int? TotalGuest { get; set; }
        public int? TotalFbclick { get; set; }
        public int? TotalInsClick { get; set; }
        public int? TotalTwiClick { get; set; }
        public int? TotalLinClick { get; set; }
        public int? TotalPinClick { get; set; }
    }
}