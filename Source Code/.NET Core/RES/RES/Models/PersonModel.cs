using RES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RES.Models
{
    public abstract class PersonModel
    {
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private string address;
        private AccountModel account;

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }

        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        public AccountModel Account
        {
            get { return this.account; }
            set { this.account = value; }
        }

        public PersonModel()
        {
            firstName = "";
            lastName = "";
            email = "";
            address = "";
            account = new AccountModel();
        }
    }
}
