using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstateWebsite.Models;

namespace RealEstateWebsite.Areas.Censor.Models
{
    public class PostData
    {
        public List<Direction> lstDirection { get; set; }

        public List<RealEstateType> lstRealEstateType { get; set; }

        public List<Type1> lstPostType { get; set; }

        public List<Project> lstProject { get; set; }

        public Post post { get; set; }
    }
}