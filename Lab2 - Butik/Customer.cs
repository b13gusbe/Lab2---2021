using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2___Butik
{
    [Serializable]
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

        public int CartCount()
        {
            return this.cart.Count;
        }

        public int CartCost(string currency)
        {
            int totalCost = 0;
            foreach (Product product in this.cart)
            {
                totalCost += product.Price(currency);
            }
            return totalCost;
        }


        public override string ToString()
        {
            string s = $"Username: {this.username} - Password: {this.password} - Cart: ";
            foreach (Product product in this.cart)
            {
                s += ($"{product.name}, ");
            }
            s = s.Remove(s.Length - 2);
            return s;
        }

    }
}
