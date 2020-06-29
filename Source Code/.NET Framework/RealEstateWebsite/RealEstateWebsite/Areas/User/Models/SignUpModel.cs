using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Areas.User.Models
{
    public class SignUpModel
    {
        [Key]
        public long ID { set; get; }

        [Display(Name ="UserName")]
        [Required(ErrorMessage ="Username requried")]
        public String UserName { set; get; }

        [Display(Name = "Password")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="Password length is least six character")]
        [Required(ErrorMessage = "Password required")]
        public String PassWord { set; get; }

        [Display(Name = "Confirm Password")]
        [Compare("PassWord",ErrorMessage ="Confirm password")]
        public String ConfirmPassWord { set; get; }

        [Required(ErrorMessage = "FirstName required")]
        public String FirstName { set; get; }

        [Required(ErrorMessage = "LastName required")]
        public String LastName { set; get; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email required")]
        public String Email { set; get; }

        [Display(Name = "PhoneNumber")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Password length is least ten character")]
        [Required(ErrorMessage = "PhoneNumber required")]
        public String PhoneNumber { set; get; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address required")]
        public String Address { set; get; }
        [Display(Name = "Images")]
        public string Avatar_URL { get; set; }

    }
}