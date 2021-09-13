using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2___Butik
{
    class Product
    {
        public string name { get; set; }
        private int price { get; set; }

        public Product(string name, int price)
        {
            this.name = name;
            this.price = price;
        }

        public override string ToString()
        {
            return name.ToString();
        }

        public int Price()
        {
            return this.price;
        }



    }
}
