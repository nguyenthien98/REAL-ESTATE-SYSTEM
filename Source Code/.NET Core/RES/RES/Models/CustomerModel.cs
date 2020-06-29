using RES.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RES.Models
{
    public class CustomerModel : PersonModel
    {
        private RealEstateSystemContext db;

        public CustomerModel() : base()
        {
            db = new RealEstateSystemContext();
        }
    }
}
