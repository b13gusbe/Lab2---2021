using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2___Butik
{
    class Customer
    {
        public string username { get; private set; }
        private string password { get; set; }

        private List<Product> cart;
        public Customer(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.cart = new List<Product>();
        }






    }
}
