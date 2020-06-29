using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Areas.Admin.Models
{
    public  class Dashboard
    {
        public Dashboard()
        {

        }
     
        private int count_Employee;
        
        private int count_Customer;
        private int count_Post;
        private double sum_Sale;

        public int Count_Employee
        {
            get
            {
                return count_Employee;
            }

            set
            {
                count_Employee = value;
            }
        }

        public int Count_Customer
        {
            get
            {
                return count_Customer;
            }

            set
            {
                count_Customer = value;
            }
        }

        public int Count_Post
        {
            get
            {
                return count_Post;
            }

            set
            {
                count_Post = value;
            }
        }

        public double Sum_Sale
        {
            get
            {
                return sum_Sale;
            }

            set
            {
                sum_Sale = value;
            }
        }
    }
}