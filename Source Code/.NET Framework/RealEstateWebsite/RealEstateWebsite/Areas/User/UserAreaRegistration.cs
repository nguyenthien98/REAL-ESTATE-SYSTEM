using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateWebsite.Areas.User
{
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "RealEstateWebsite.Areas.User.Controllers" }
            );
        }
    }
}