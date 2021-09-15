using System;
using System.Collections.Generic;

namespace Lab2___Butik
{
    class Program
    {
        static string p1 = "############################################################################";
        static string p2 = "#                                                                          #";
        static string p3 = "#         ";

        static void Main(string[] args) {

            Customer testCustomer = new Customer("Testson", "123");

            List<Product> allProducts = new List<Product>();
            List<Customer> allCustomers = new List<Customer>();

            allProducts.Add(new Product("Apple", 1));
            allProducts.Add(new Product("Orange", 5));
            allProducts.Add(new Product("Banana", 2));
            allProducts.Add(new Product("Papaya", 25));
            allProducts.Add(new Product("Kiwi", 13));

            string currency = "$";


            allCustomers.Add(testCustomer);


            //Console.WriteLine(allProducts[0].ToString());
            
            int menuChoice = drawMenu();
            Customer logedInCustomer;

            if(menuChoice == 1) { drawStore(allProducts, logedInCustomer = drawLogIn(allCustomers), currency); }
            else if (menuChoice == 2) { Console.WriteLine("SKAPA ANVÄNDARE inte impelenterad"); }
            else if (menuChoice == 3) { Environment.Exit(0); }

            //drawStore(allProducts, logedInCustomer);


            /*
            Customer logedInCustomer = drawLogIn(allCustomers);
            logedInCustomer.AddToCart(allProducts[0]);
            Console.WriteLine(logedInCustomer.cart[0].name);
            */

            //drawStore(allProducts, testCustomer);

            
            /*
            testCustomer.AddToCart(allProducts[2]);
            testCustomer.AddToCart(allProducts[2]);
            
            testCustomer.AddToCart(allProducts[0]);
            testCustomer.AddToCart(allProducts[0]);
            testCustomer.AddToCart(allProducts[0]);
            testCustomer.AddToCart(allProducts[0]);
            testCustomer.AddToCart(allProducts[0]);
            */
            drawViewCart(testCustomer, currency);


        }


        private static int drawMenu()
        {
            ConsoleKeyInfo cki;
            //string p1 = "############################################################################\n";
            //string p2 = "#                                                                          #\n";
            Console.Clear();
            Console.WriteLine(p1 + "\n" + p2 +  "\n" + p2 + "\n#             Welcome to this \"online\" luxurious fruit store               #\n" + p2 + "\n" + p2);
            Console.WriteLine(p3 + "Please select one of the following alternetives:                 #\n" + p2);
            Console.WriteLine(p3 + "(1) Log in                                                       #");
            Console.WriteLine(p3 + "(2) Create user                                                  #");
            Console.WriteLine(p3 + "(3) Exit store                                                   #\n" + p2 + "\n" + p1);

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

        private static Customer drawLogIn(List<Customer> allCustomers)
        {
            ConsoleKeyInfo cki;
            Console.Clear();
            Console.WriteLine(p1 + "\n" + p2 + "\n" + p2 + "\n" + p2 + "\n#                       Please enter your username                         #\n" + p2 + "\n" + p2 + "\n" + p2 + "\n" + p1);

            do
            {
                string nameInput = Console.ReadLine();
                Customer logedInCustomer = allCustomers.Find(x => x.username == nameInput);

                if(logedInCustomer != null){

                    Console.WriteLine("\nPlease enter password for {0}:", nameInput);
                    string passwordInput = Console.ReadLine();
                    if (logedInCustomer.LogIn(passwordInput))
                    {
                        Console.WriteLine("Log in lyckades.");
                        return logedInCustomer;
                    }
                    else { Console.WriteLine("Log in FAIL"); }


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

        private static void drawStore(List<Product> allProducts, Customer logedInCustomer, string currency)
        {
            ConsoleKeyInfo cki;
            do
            {
                Console.Clear();
                Console.WriteLine(p1 + "\n" + p2 + "\n" + p3 + "    The store currently offers the following products:           #" + "\n" + p2);
                for (int i = 0; i < allProducts.Count; i++)
                {
                    if (i > 8) { Console.Write(p3 + "(" + (i + 1) + ") " + allProducts[i].name); }
                    else { Console.Write(p3 + "(" + (i + 1) + ") " + allProducts[i].name); }
                    for (int i2 = 0; i2 < 48 - allProducts[i].name.Length; i2++)
                    {
                        Console.Write(".");
                    }
                    if (allProducts[i].Price(currency) < 10) { Console.Write("$" + allProducts[i].Price(currency) + "           #\n"); }
                    else if (allProducts[i].Price(currency) < 100) { Console.Write("$" + allProducts[i].Price(currency) + "          #\n"); }
                    else { Console.Write("$" + allProducts[i].Price(currency) + "         #\n"); }
                    
                }
                Console.Write(p2 + "\n" + p3 + "(" + (allProducts.Count + 1) + ") Leave store                           (" + (allProducts.Count + 2) + ") View Cart          #\n" + p2 + "\n" + p1);
                Console.WriteLine("\nLogged in as: " + logedInCustomer.username + "                           Number of items in cart: " + logedInCustomer.CartCount() + "\n");
                cki = Console.ReadKey();

                if (char.IsDigit(cki.KeyChar))
                {
                    int i = int.Parse(cki.KeyChar.ToString());
                    if (i > 0 && i < (allProducts.Count + 1))
                    {
                        logedInCustomer.AddToCart(allProducts[i-1]);
                    } else if ( i == allProducts.Count + 1)
                    {
                        break;
                    } else if ( i == allProducts.Count + 2)
                    {
                        drawViewCart(logedInCustomer, currency);
                    }
                }


            } while (true);

        }


        private static void drawViewCart(Customer customer, string currency)
        {
            Console.Clear();
            Console.Write(p1 + "\n" + p2 + "\n" + p3 + "    Items in cart:                                               #\n" + p2 + "\n");

            for (int i = 0; i < customer.CartCount(); i++)
            {
                if (i > 8) { Console.Write("#        (" + (i + 1) + ") " + customer.cart[i].name); }
                else { Console.Write(p3 + "(" + (i + 1) + ") " + customer.cart[i].name); }
                for (int i2 = 0; i2 < 48 -customer.cart[i].name.Length; i2++)
                {
                    Console.Write(".");
                }
                if(customer.cart[i].Price(currency) < 10) { Console.Write("$" + customer.cart[i].Price(currency) + "           #\n"); }
                else if (customer.cart[i].Price(currency) < 100) { Console.Write("$" + customer.cart[i].Price(currency) + "          #\n"); }
                else { Console.Write("$" + customer.cart[i].Price(currency) + "         #\n"); }
                
            }
            int cartCost = customer.CartCost(currency);
            if (cartCost < 10) { Console.Write(p2 + "\n#                                                 Total cost: ${0}           #\n", cartCost); }
            else if (cartCost < 100) { Console.Write(p2 + "\n#                                                Total cost: ${0}           #\n", cartCost); }
            else if (cartCost < 1000) { Console.Write(p2 + "\n#                                               Total cost: ${0}           #\n", cartCost); }

            Console.Write(p2 + "\n" + p3 + "({0}) Go back to Store                  ({1}) Purchase items         #\n" + p2 + "\n" + p1, customer.CartCount()+1, customer.CartCount()+2);


            do
            {


            } while (true);
            
            
        }



    }
}





