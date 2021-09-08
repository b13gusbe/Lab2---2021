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
            allProducts.Add(new Product("Papaya", 25));




            //Console.WriteLine(allProducts[0].ToString());

            int menuChoice = drawMenu();


        }


        private static int drawMenu()
        {
            ConsoleKeyInfo cki;
            string p1 = "############################################################################\n";
            string p2 = "#                                                                          #\n";

            Console.WriteLine(p1 + p2 + p2 + p2 + "#             Welcome to this \"online\" luxurious fruit store               #\n" + p2 + "#                                                                          #");
            Console.WriteLine("#             Please select one of the following alternetives:             #\n" + "#                                                                          #");
            Console.WriteLine("#             (1) Log in                                                   #");
            Console.WriteLine("#             (2) Create user                                              #");
            Console.WriteLine("#             (3) Exit store                                               #\n" + p2 + p1);

            do
            {
                cki = Console.ReadKey();
                if(cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2 && cki.Key != ConsoleKey.D3)
                {
                    Console.WriteLine(" Please enter one of the listed alternatives.");
                } else if(cki.Key == ConsoleKey.D1){ return 1; }
                else if(cki.Key == ConsoleKey.D2) { return 2; }
                else if (cki.Key == ConsoleKey.D3) { Environment.Exit(0); }
            } while (true);

        }

        private static void drawStore(List<Product> allProducts, List<Customer> allCustomers)
        {

        }



    }
}





