using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab2___Butik
{
    class Program
    {
        static string p1 = "############################################################################\n";
        static string p2 = "          ";

        static readonly string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SavedCustomers.doesitreallymatter");

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

            /*
            List<Customer> savedCustomers = LoadCustomers();
            allCustomers.AddRange(savedCustomers);

            foreach(Customer customer in allCustomers)
            {
                Console.Write($"{customer.username} : ");
                foreach(Product product in customer.cart)
                {
                    Console.Write($"{product.name} ");
                }
                Console.Write("\n");
            }

            Console.ReadKey();

            */


            //Console.WriteLine(allProducts[0].ToString());


            //DrawCreateUser(allCustomers);

            /*
            int menuChoice = drawMainMenu();
            Customer logedInCustomer;

            if(menuChoice == 1) { drawStore(allProducts, logedInCustomer = drawLogIn(allCustomers), currency); }
            else if (menuChoice == 2) { Console.WriteLine("SKAPA ANVÄNDARE inte impelenterad"); }
            else if (menuChoice == 3) { Environment.Exit(0); }
            */





            //drawStore(allProducts, logedInCustomer);


            /*
            Customer logedInCustomer = drawLogIn(allCustomers);
            logedInCustomer.AddToCart(allProducts[0]);
            Console.WriteLine(logedInCustomer.cart[0].name);
            */

            //drawStore(allProducts, testCustomer);

            DrawMainMenu(allCustomers, allProducts, currency);


            
            testCustomer.AddToCart(allProducts[2]);
            testCustomer.AddToCart(allProducts[2]);
            
            testCustomer.AddToCart(allProducts[0]);
            testCustomer.AddToCart(allProducts[0]);
            testCustomer.AddToCart(allProducts[1]);
            testCustomer.AddToCart(allProducts[3]);
            testCustomer.AddToCart(allProducts[0]);

            DrawCart(testCustomer, currency);

            //SaveCustomers(allCustomers);

            //drawViewCart(testCustomer, currency);


        }


        private static void DrawMainMenu(List<Customer> allCustomers, List<Product> allProducts, string currency)
        {
            ConsoleKeyInfo cki;

            do
            {
                Console.Clear();
                Console.WriteLine(p1 + "\n               Welcome to this \"online\" luxurious fruit store\n");
                Console.WriteLine(p2 + "Please select one of the following alternetives:\n");
                Console.WriteLine(p2 + "(1) Log in");
                Console.WriteLine(p2 + "(2) Create new customer account");
                Console.WriteLine(p2 + "(3) Exit store\n\n" + p1);

                cki = Console.ReadKey();
                if(cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2 && cki.Key != ConsoleKey.D3)
                {
                    Console.WriteLine(" Please enter one of the listed alternatives.");
                } else if(cki.Key == ConsoleKey.D1){ DrawLogIn(allCustomers, allProducts, currency); }
                else if(cki.Key == ConsoleKey.D2) { DrawCreateUser(allCustomers, allProducts, currency); }
                else if (cki.Key == ConsoleKey.D3) { Environment.Exit(0); }
            } while (true);

        }

        private static void DrawLogIn(List<Customer> allCustomers, List<Product> allProducts, string currency)
        {
            Console.Clear();
            Console.WriteLine(p1 + "\n\n                        Please enter your username                          \n\n\n" + p1);

            ConsoleKeyInfo cki;

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
                        DrawLogedInMenu(allCustomers, allProducts, logedInCustomer, currency);
                    }
                    else { Console.WriteLine("Password is incorrect."); }


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

        private static void DrawCreateUser(List<Customer> allCustomers, List<Product> allProducts, string currency)
        {
            Console.Clear();
            Console.WriteLine(p1 + "\n\n                              Create new customer\n\n\n" + p1);
            
            do
            {
                Console.Write("Enter new Username: ");
                string username = Console.ReadLine();
                Customer logedInCustomer = allCustomers.Find(customer => customer.username == username);
                if (logedInCustomer != null)
                {
                    Console.WriteLine($"The username {username} already exists.\n\n");
                } else
                {
                    Console.Write("Enter new Password: ");
                    string password = Console.ReadLine();
                    allCustomers.Add(new Customer(username, password));
                    SaveCustomers(allCustomers);

                    Console.Clear();
                    Console.WriteLine(p1 + "\n\n                         Customer Successfully created\n\n");
                    Console.WriteLine($"               Username: {username}");
                    Console.WriteLine($"               Password: {password}\n\n\n" + p1);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                }

            } while (true);

            
        }

        private static void DrawLogedInMenu(List<Customer> allCustomers, List<Product> allProducts, Customer logedInCustomer,  string currency)
        {
            do
            {
                Console.Clear();
                Console.WriteLine(p1 + $"\n              Welcome {logedInCustomer.username}\n\n");
                Console.WriteLine(p2 + "Please select one of the options:\n");
                Console.WriteLine(p2 + "(1) Shop luxurios fruit");
                Console.WriteLine(p2 + "(2) View cart");
                Console.WriteLine(p2 + "(3) Go to checkout");

                ConsoleKeyInfo cki;

                do
                {
                    cki = Console.ReadKey();
                    if(cki.Key == ConsoleKey.D1) { DrawStore(allProducts, logedInCustomer, currency); break; }
                    else if (cki.Key == ConsoleKey.D2) { DrawCart(logedInCustomer, currency); break; }
                    else if (cki.Key == ConsoleKey.D3) { DrawCheckout(logedInCustomer, currency); break; }
                    else { Console.WriteLine("Please select one of the three options."); }
                    
                } while (true);

            } while (true);
        }

        private static void DrawStore(List<Product> allProducts, Customer logedInCustomer, string currency)
        {
            ConsoleKeyInfo cki;
            do
            {
                Console.Clear();
                Console.WriteLine(p1 + "\n"  + p2 + "    The store currently offers the following products:           " + "\n");
                for (int i = 0; i < allProducts.Count; i++)
                {
                    if (i > 8) { Console.Write(p2 + "(" + (i + 1) + ") " + allProducts[i].name); }
                    else { Console.Write(p2 + "(" + (i + 1) + ") " + allProducts[i].name); }
                    for (int i2 = 0; i2 < 48 - allProducts[i].name.Length; i2++)
                    {
                        Console.Write(".");
                    }
                    if (allProducts[i].Price(currency) < 10) { Console.Write("$" + allProducts[i].Price(currency) + "\n"); }
                    else if (allProducts[i].Price(currency) < 100) { Console.Write("$" + allProducts[i].Price(currency) + "\n"); }
                    else { Console.Write("$" + allProducts[i].Price(currency) + "#\n"); }
                    
                }
                Console.Write(p2 + "\n" + p2 + "(" + (allProducts.Count + 1) + ") Leave store                           (" + (allProducts.Count + 2) + ") View Cart\n\n" + p1);
                Console.WriteLine("\nLogged in as: " + logedInCustomer.username + "                           Number of items in cart: " + logedInCustomer.CartCount() + "\n");
                cki = Console.ReadKey();

                if (char.IsDigit(cki.KeyChar))
                {
                    int i = int.Parse(cki.KeyChar.ToString());
                    if (i > 0 && i < (allProducts.Count + 1))
                    {
                        logedInCustomer.AddToCart(allProducts[i-1]);
                    }
                } else if(cki.Key == ConsoleKey.X)
                {
                    break;
                } else if (cki.Key == ConsoleKey.C)
                {
                    DrawCart(logedInCustomer, currency);
                }


            } while (true);

        }

        private static void DrawCart(Customer logedInCustomer, string currency)
        {

            ConsoleKeyInfo cki;

            do
            {

                Console.Clear();
                Console.WriteLine(p1 + "                                                         (C)urrency");
                Console.WriteLine($"                           {logedInCustomer.username}'s Cart:\n\n");

                List<Product> cartItems = logedInCustomer.cart.OrderBy(item => item.name).ToList();

                List<Product> printItems;
                String s;
                double totalPrice = 0;
                

                do
                {
                    printItems = cartItems.FindAll(products => products.name == cartItems[0].name);
                    Console.Write(s = (p2 + $"{printItems[0].name} (x{printItems.Count()})"));

                    for (int i = 0; i < 54 - s.Length; i++)
                    {
                        Console.Write(".");
                    }
                    Console.Write($"({currency}{printItems[0].Price(currency)} e.a)");
                    Console.WriteLine($"\n                                                            {printItems.Count()}x  {currency}{printItems.Count() * printItems[0].Price(currency)}\n");

                    totalPrice += printItems.Count() * printItems[0].Price(currency);

                    cartItems.RemoveRange(0, printItems.Count());


                } while (cartItems.Count != 0);

                Console.Write($"\n                                                         Total: {currency}{totalPrice}\n");


                Console.Write("                                                 Bronze member: -5%\n\n");
                Console.WriteLine($"                                                     New Total: {currency}{totalPrice * 0.95}\n");


                //if (logedInCustomer == bronze)
                //{
                //    Console.WriteLine("Bronze member: -5%\n\n");
                //    Console.WriteLine($"New Total: {currency}{totalPrice*0.95}");
                //}
                //else if (logedInCustomer == silver)
                //{
                //    Console.WriteLine("Silver member: -10%\n\n");
                //    Console.WriteLine($"New Total: {currency}{totalPrice * 0.90}");
                //}
                //else if (logedInCustomer == gold)
                //{
                //    Console.WriteLine("Gold member: -15%\n\n");
                //    Console.WriteLine($"New Total: {currency}{totalPrice * 0.85}");
                //}

                Console.WriteLine("\n           (G)o Back                                 (B)uy Fruits\n\n" + p1);

                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.C)
                {
                    if (currency == "$") { currency = "SEK "; }
                    else if (currency == "SEK ") { currency = "£"; }
                    else if(currency == "£") { currency = "$"; }
                } else if (cki.Key == ConsoleKey.G)
                {
                    break;
                } else if (cki.Key == ConsoleKey.B)
                {
                    Console.WriteLine("You bought all the fruits! Congratulations!! You Won!!!");
                    Console.ReadKey();
                }



                
            } while (true);
            
        }

        private static void DrawCheckout(Customer logedInCustomer, string currency)
        {

        }



        private static void SaveCustomers(List<Customer> customers)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, customers);
            stream.Close();
        }

        private static List<Customer> LoadCustomers()
        {
            List<Customer> customers = new List<Customer>();

            if (File.Exists(filePath))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                customers = (List<Customer>)formatter.Deserialize(stream);
                stream.Close();
            }

            return customers;
        }



    }
}





