using System;

namespace Lab2___Butik
{
    [Serializable]
    class Product
    {
        public string name { get; set; }
        private double price { get; set; }

        public Product(string name, int price)
        {
            this.name = name;
            this.price = price;
        }

        public double Price(string currency)
        {
            if (currency == "$")
            {
                return (this.price * 1.35);
            } else if (currency == "SEK ")
            {
                return (this.price * 11.87);
            } else if (currency == "£") 
            {
                return this.price;
            } else
            {
                return 0;
            }
            
        }
    }
}
