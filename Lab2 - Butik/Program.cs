using System;
using System.Collections.Generic;

namespace Lab2___Butik
{
    class Program
    {
        static void Main(string[] args) {

            Customer testCustomer = new Customer("Testson", "123");

            List<Product> allProducts = new List<Product>();
            List<Customer> allCustomers = new List<Customer>();

            allProducts.Add(new Product("Apple", 10));
            allProducts.Add(new Product("Orange", 15));
            allProducts.Add(new Product("Banana", 20));


            //Console.WriteLine(allProducts[0].ToString());

            allCustomers = drawLoginScreen(allCustomers);


        }


        private static List<Customer> drawLoginScreen(List<Customer> allCustomers)
        {
            string p1 = "############################################################################";
            string p2 = "#                                                                          #";
            Console.WriteLine(p1 + "\n" + p2 + "\n" + p2 + "\n" + p2 + "\n#             Welcome to this \"online\" luxurious fruit store               #" + "\n" + p2 + "\n" + p2 + "\n" + p2);
            









            return allCustomers;
        }

        private static void drawStore(List<Product> allProducts, List<Customer> allCustomers)
        {

        }



    }
}





