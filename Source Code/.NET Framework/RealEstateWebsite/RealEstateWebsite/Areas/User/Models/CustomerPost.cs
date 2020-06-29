using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Areas.User.Models
{
    public class CustomerPost
    {
        
        [Display(Name = "RealEstateType(*)")]
        [Required(ErrorMessage = "RealEstateType requried")]
        public Nullable<int> RealEstaleType { set; get; }
        [Display(Name = "PostType(*)")]
        [Required(ErrorMessage = "PostType requried")]
        public Nullable<int> PostType { get; set; }
        [Display(Name = "Tittle(*)")]
        [Required(ErrorMessage = "Tittle requried")]
        public String Tittle { set; get; }

        [Display(Name = "Price(*)")]
        [Required(ErrorMessage = "Price requried")]
        public decimal Price { set; get; }

        [Display(Name = "LocationPost(*)")]
        [Required(ErrorMessage = "LocationPost requried")]
        public String Location { set; get; }

        [Display(Name = "Area(*)")]
        [Required(ErrorMessage = "Area requried")]
        public decimal Area { set; get; }

        [Display(Name = "Description(*)")]
        public String Description { set; get; }

        [Display(Name = "ImagesPost(*)")]
        public String url { set; get; }

        //Project
        [Display(Name = "ProjectName(*)")]
        public String ProjectName { get; set; }
        [Display(Name = "Protential(*)")]
        public String Protential { get; set; }

        [Display(Name = "LocationProject(*)")]
        [Required(ErrorMessage = "LocationProject requried")]
        public String LocationProject { set; get; }
        [Display(Name = "ImagesProject(*)")]
        public String url1 { set; get; }
        [Display(Name = "Floor")]
        public Nullable<int> Floor { set; get; }

        [Display(Name = "Bedroom")]
        public Nullable<int> Bedroom { set; get; }

        [Display(Name = "Bathroom")]
        public Nullable<int> Bathroom { set; get; }

        [Display(Name = "Direction")]
        public Nullable<int> Direction { set; get; }
    }
}
   
    