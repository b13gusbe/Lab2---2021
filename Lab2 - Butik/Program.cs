using System;
using System.Collections.Generic;

namespace Lab2___Butik
{
    class Program
    {
        static string p1 = "############################################################################\n";
        static string p2 = "#                                                                          #\n";

        static void Main(string[] args) {

            Customer testCustomer = new Customer("Testson", "123");

            List<Product> allProducts = new List<Product>();
            List<Customer> allCustomers = new List<Customer>();

            allProducts.Add(new Product("Apple", 10));
            allProducts.Add(new Product("Orange", 15));
            allProducts.Add(new Product("Banana", 20));
            allProducts.Add(new Product("Papaya", 25));

            allCustomers.Add(testCustomer);


            //Console.WriteLine(allProducts[0].ToString());
            /*
            int menuChoice = drawMenu();

            if(menuChoice == 1) { drawLogIn(allCustomers); }
            else if (menuChoice == 2) { }
            */

            drawLogIn(allCustomers);
        }


        private static int drawMenu()
        {
            ConsoleKeyInfo cki;
            //string p1 = "############################################################################\n";
            //string p2 = "#                                                                          #\n";
            Console.Clear();
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

        private static void drawLogIn(List<Customer> allCustomers)
        {
            ConsoleKeyInfo cki;
            Console.Clear();
            Console.WriteLine(p1 + p2 + p2 + p2 + "#                       Please enter your username                         #\n" + p2 + p2 + p2 + p1);

            do
            {
                string nameInput = Console.ReadLine();
                Customer logedInCustomer = allCustomers.Find(x => x.username == nameInput);

                if(logedInCustomer != null){

                    Console.WriteLine("\nPlease enter password for {0}:", nameInput);
                    string passwordInput = Console.ReadLine();


                }
                else if(nameInput != ""){
                    Console.WriteLine("\nUnable to find {0}. Would you like to add this user? (Y/N)", nameInput);
                    do
                    {

                        cki = Console.ReadKey();
                        if(cki.Key == ConsoleKey.Y)
                        {
                            Console.WriteLine("\n\nChoose a password for new user {0}.", nameInput);
                            string password = Console.ReadLine();
                            Customer newCustomer = new Customer(nameInput, password);
                            allCustomers.Add(newCustomer);
                            Console.WriteLine("\nPassword has been set to: {0} \nYou are now able to log in with user: {1}\n\nPlease enter username: ", password, nameInput);
                            break;
                        }else if(cki.Key == ConsoleKey.N)
                        {
                            Console.WriteLine("\nEnter username: ");
                            break;
                        }else { Console.WriteLine("\nPlease enter Y or N"); }


                    } while (true);

                
                }



            } while (true);

        }

        private static void drawStore(List<Product> allProducts, List<Customer> allCustomers)
        {

        }



    }
}





