//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealEstateWebsite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            this.Interested_Post = new HashSet<Interested_Post>();
            this.Promotion_Detail = new HashSet<Promotion_Detail>();
            this.Post_Image = new HashSet<Post_Image>();
            this.Post_Report = new HashSet<Post_Report>();
            this.Post_Status = new HashSet<Post_Status>();
        }
    
        public int Post_ID { get; set; }
        public Nullable<System.DateTime> PostTime { get; set; }
        public string Tittle { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public decimal Area { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Interested_Post> Interested_Post { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Promotion_Detail> Promotion_Detail { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Detail Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post_Image> Post_Image { get; set; }
        public virtual Type1 Type1 { get; set; }
        public virtual Project Project { get; set; }
        public virtual RealEstateType RealEstateType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post_Report> Post_Report { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post_Status> Post_Status { get; set; }
    }
}
