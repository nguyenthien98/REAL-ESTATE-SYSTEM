using System;
using System.Collections.Generic;

namespace RES.Data.DBModels
{
    public partial class Customer
    {
        public Customer()
        {
            Block = new HashSet<Block>();
            Post = new HashSet<Post>();
        }

        public int CustomerId { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountId { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual AspNetUsers Account { get; set; }
        public virtual ICollection<Block> Block { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}