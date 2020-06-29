using RES.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RES.Models
{
    public class AccountModel
    {
        private RealEstateSystemContext db;
        private string username;
        private string password;
        private bool? isSuperAdmin;

        public string UserName
        {
            get { return this.username; }
            set { this.username = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public bool? IsSuperAdmin
        {
            get { return this.isSuperAdmin; }
            set { this.isSuperAdmin = value; }
        }

        internal Customer GetCustomerByUserNamePassword()
        {
            throw new NotImplementedException();
        }

        public AccountModel()
        {
            db = new RealEstateSystemContext();
        }

        public AccountModel(string username, string password)
        {
            db = new RealEstateSystemContext();
            this.username = username;
            this.password = password;
        }
    }
}
