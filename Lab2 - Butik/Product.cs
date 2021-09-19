using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2___Butik
{
    [Serializable]
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

        public int Price(string currency)
        {
            if (currency == "$")
            {
                return this.price;
            } else if (currency == "SEK ")
            {
                return (this.price * 10);
            } else if (currency == "£") 
            {
                return (this.price * 11);
            } else
            {
                return 0;
            }
            
        }



    }
}
