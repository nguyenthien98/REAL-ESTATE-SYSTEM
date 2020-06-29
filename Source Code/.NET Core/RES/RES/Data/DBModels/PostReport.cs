using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class PostReport
    {
        public int PostReportId { get; set; }
        public string PostId { get; set; }
        public string Reason { get; set; }
        public DateTime? ReportTime { get; set; }
        public int? Reporter { get; set; }
        public int? Status { get; set; }

        public virtual Post Post { get; set; }
    }
}