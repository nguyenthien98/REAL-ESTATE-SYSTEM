using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Areas.Censor.Models
{
    public class DashboardData
    {
        public struct ChartData
        {
            public string period;
            public int posts;
            public int usersOnline;
        }

        public int totalPostsToday { get; set; }
        public int totalPendingPosts { get; set; }
        public int totalReportedPosts { get; set; }
        public int totalReportedCustomers { get; set; }
        public List<ChartData> chart { get; set; }
    }
}