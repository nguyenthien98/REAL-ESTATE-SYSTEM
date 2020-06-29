using RealEstateWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebsite.Areas.User.BLL
{
    public class BLLAccount
    {
        //cái dbcontext là gì
        RealEstateWebsiteEntities _dbContext;
        public BLLAccount()
        {
            _dbContext = new RealEstateWebsiteEntities();
        }
        public long ThemAC(Account model)
        {
            _dbContext.Accounts.Add(model);
            _dbContext.SaveChanges();
            return model.Account_ID;
        }
        public long ThemCT(Customer model)
        {
            _dbContext.Customers.Add(model);
            _dbContext.SaveChanges();
            return model.Customer_ID;
        }
    }
}