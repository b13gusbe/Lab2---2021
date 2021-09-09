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

        public List<Product> cart;
        public Customer(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.cart = new List<Product>();
        }


        public bool LogIn (string password)
        {
            if(this.password == password) { return true; }
            else { return false; }
        }

        public void AddToCart( Product product)
        {
            this.cart.Add(product);
            Console.WriteLine("{0} added to cart.", product.name);
        }





    }
}
