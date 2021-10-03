using System;
using System.Collections.Generic;

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
        }

        public void ClearCart()
        {
            this.cart.Clear();
        }

        public int CartCount()
        {
            return this.cart.Count;
        }

        public double CartCost(string currency)
        {
            double totalCost = 0;
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
