using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateTesting.Banque
{
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Customer(string name, string email)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            this.Name = name;
            this.Email = email;
        }

        public Customer()
        {

        }
    }
}