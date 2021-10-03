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

            Customer customer1 = new BronzeCustomer("Knatte", "123");
            Customer customer2 = new SilverCustomer("Fnatte", "321");
            Customer customer3 = new GoldCustomer("Tjatte", "213");

            List<Product> allProducts = new List<Product>();
            List<Customer> allCustomers = new List<Customer>();

            allProducts.Add(new Product("Apple", 2));
            allProducts.Add(new Product("Orange", 5));
            allProducts.Add(new Product("Banana", 4));
            allProducts.Add(new Product("Papaya", 13));
            allProducts.Add(new Product("Kiwi", 10));

            allCustomers.Add(customer1);
            allCustomers.Add(customer2);
            allCustomers.Add(customer3);

            if (File.Exists(filePath)){
                allCustomers = LoadCustomers();
            }

            DrawMainMenu(allCustomers, allProducts);

        }


        private static void DrawMainMenu(List<Customer> allCustomers, List<Product> allProducts)
        {
            ConsoleKeyInfo cki;

            do
            {
                Console.Clear();
                Console.WriteLine(p1 + "\n               Welcome to this \"online\" luxuriöus fruit store\n");
                Console.WriteLine(p2 + "Please select one of the following alternatives:    ,--./,-.");
                Console.WriteLine("                                                             / #      \\");
                Console.WriteLine(p2 + "(1) Log in                                        |          |");
                Console.WriteLine(p2 + "(2) Create new customer account                    \\        /");
                Console.WriteLine(p2 + "(3) Exit store                                      `._,._,'\n\n" + p1);

                DrawBorders(9);
                
                cki = Console.ReadKey();

                if(cki.Key == ConsoleKey.D1){ DrawLogIn(allCustomers, allProducts); }
                else if(cki.Key == ConsoleKey.D2) { DrawCreateUser(allCustomers); }
                else if (cki.Key == ConsoleKey.D3) { Environment.Exit(0); }

            } while (true);

        }

        public static void DrawLogIn(List<Customer> allCustomers, List<Product> allProducts)
        {
            Console.Clear();
            Console.WriteLine(p1 + "\n           .-.                     Log in                    .-.");
            Console.WriteLine("          /  |                                               |  \\");
            Console.WriteLine("         |  /      Username:                                  \\  |");
            Console.WriteLine("      .'\\|.-; _    Password:                                _ ;-.|/'.");
            Console.WriteLine("     /.-.;\\  |\\|                                           |/|  /;.-.\\");
            Console.WriteLine("     '   |'._/ `                                           ´ \\_.'|   '");
            Console.WriteLine("         |  \\                                                 /  |");
            Console.WriteLine("          \\  |      Go back                                  |  /");
            Console.WriteLine("           '-'       (ESC)                                   '-'\n\n" + p1);

            DrawBorders(11);

            bool userNameAccepted = false;

            string username = "";
            string password = "";

            ConsoleKeyInfo cki;

            do
            {

                if (!userNameAccepted)
                {
                    Console.SetCursorPosition(29, 4);
                    Console.Write(username + "                  ");
                    Console.SetCursorPosition((29 + username.Length), 4);
                }
                else
                {
                    Console.SetCursorPosition(29, 5);
                    string hiddenPass = new string('*', password.Length);
                    Console.Write(hiddenPass + "                  ");
                    Console.SetCursorPosition((29 + password.Length), 5);
                }

                cki = Console.ReadKey();
                if (char.IsLetterOrDigit(cki.KeyChar))
                {
                    if (!userNameAccepted)
                    {
                        username = username + cki.KeyChar;
                    }
                    else
                    {
                        password = password + cki.KeyChar;
                    }
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    if (!userNameAccepted)
                    {
                        if (username != "")
                        {
                            username = username.Remove(username.Length - 1);
                        }
                    }
                    else
                    {
                        if (password != "")
                        {
                            password = password.Remove(password.Length - 1);
                        }
                    }
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    Customer logedInCustomer = allCustomers.Find(x => x.username == username);

                    if (!userNameAccepted)
                    {
                        if (logedInCustomer != null)
                        {
                            userNameAccepted = true;
                        }
                        else if (username != "")
                        {
                            Console.SetCursorPosition(19, 7);
                            Console.WriteLine($"The username {username} does not exist");
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Would you like to go to the create customer menu? (Y/N)");

                            cki = Console.ReadKey();
                            if (cki.Key == ConsoleKey.Y)
                            {
                                DrawCreateUser(allCustomers);
                                break;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.WriteLine("                                                       ");
                            }
                        }
                    }
                    else
                    {
                        if (logedInCustomer.LogIn(password))
                        {
                            username = "";
                            password = "";
                            userNameAccepted = false;

                            DrawLogedInMenu(allCustomers, allProducts, logedInCustomer);
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(19, 7);
                            Console.WriteLine($"The password you entered is incorrect.");

                            password = "";
                        }
                    }
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("BUY MORE FRUIT!");
                    break;
                }
            } while (true);
        }

        public static void DrawCreateUser(List<Customer> allCustomers)
        {
            Console.Clear();
            Console.WriteLine(p1 + "\n                       Create new customer account");
            Console.WriteLine("                                                      (Tab)");
            Console.WriteLine("                                                 Membership Type:");
            Console.WriteLine(p2 + "Username:");
            Console.WriteLine(p2 + "Password:                                 [ Basic ]");
            Console.WriteLine("                                                      Bronze");
            Console.WriteLine(p2 + "                                            Silver");
            Console.WriteLine("                                                      Gold");
            Console.WriteLine("            Go Back\n             (Esc)\n\n" + p1);
            DrawBorders(12);

            ConsoleKeyInfo cki;

            string memberType = "Basic";
            string username = "";
            string password = "";

            bool userNameAccepted = false;

            do
            {
                if (!userNameAccepted)
                {
                    Console.SetCursorPosition(20, 5);
                    Console.Write(username + "                  ");
                    Console.SetCursorPosition((20 + username.Length), 5);
                }
                else
                {
                    Console.SetCursorPosition(20, 6);
                    Console.Write(password + "                  ");
                    Console.SetCursorPosition((20 + password.Length), 6);
                }

                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Tab)
                {
                    switch (memberType)
                    {
                        case "Basic":
                            memberType = "Bronze";
                            Console.SetCursorPosition(52, 6);
                            Console.Write("  Basic  ");
                            Console.SetCursorPosition(52, 7);
                            Console.Write("[ Bronze ]");
                            break;

                        case "Bronze":
                            memberType = "Silver";
                            Console.SetCursorPosition(52, 7);
                            Console.Write("  Bronze  ");
                            Console.SetCursorPosition(52, 8);
                            Console.Write("[ Silver ]");
                            break;

                        case "Silver":
                            memberType = "Gold";
                            Console.SetCursorPosition(52, 8);
                            Console.Write("  Silver  ");
                            Console.SetCursorPosition(52, 9);
                            Console.Write("[ Gold ]");
                            break;

                        case "Gold":
                            memberType = "Basic";
                            Console.SetCursorPosition(52, 9);
                            Console.Write("  Gold  ");
                            Console.SetCursorPosition(52, 6);
                            Console.Write("[ Basic ]");
                            break;
                    }
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    if (!userNameAccepted)
                    {
                        if (username != "")
                        {
                            username = username.Remove(username.Length - 1);
                        }
                    }
                    else
                    {
                        if (password != "")
                        {
                            password = password.Remove(password.Length - 1);
                        }
                    }
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    if (!userNameAccepted)
                    {
                        Customer createCustomer = allCustomers.Find(customer => customer.username == username);

                        if (createCustomer != null)
                        {
                            Console.SetCursorPosition(10, 8);
                            Console.Write($"The username {username} already exists.");
                        }
                        else
                        {
                            Console.SetCursorPosition(10, 8);
                            Console.Write($"                                    ");
                            userNameAccepted = true;
                        }
                    }
                    else
                    {
                        switch (memberType)
                        {
                            case "Basic":
                                allCustomers.Add(new Customer(username, password));
                                SaveCustomers(allCustomers);
                                Console.SetCursorPosition(10, 8);
                                Console.Write($"Customer {username} Created.");
                                break;

                            case "Bronze":
                                allCustomers.Add(new BronzeCustomer(username, password));
                                SaveCustomers(allCustomers);
                                Console.SetCursorPosition(10, 8);
                                Console.Write($"Bronze customer {username} Created.");
                                break;

                            case "Silver":
                                allCustomers.Add(new SilverCustomer(username, password));
                                SaveCustomers(allCustomers);
                                Console.SetCursorPosition(10, 8);
                                Console.Write($"Silver customer {username} Created.");
                                break;

                            case "Gold":
                                allCustomers.Add(new GoldCustomer(username, password));
                                SaveCustomers(allCustomers);
                                Console.SetCursorPosition(10, 8);
                                Console.Write($"Gold customer {username} Created.");
                                break;
                        }

                        username = "";
                        password = "";
                        userNameAccepted = false;

                        Console.SetCursorPosition(20, 6);
                        Console.Write("                        ");
                    }
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("BUY MORE FRUIT!");
                    break;
                }
                else if (char.IsLetterOrDigit(cki.KeyChar))
                {
                    if (!userNameAccepted)
                    {
                        username = username + cki.KeyChar;
                    }
                    else
                    {
                        password = password + cki.KeyChar;
                    }
                }
            } while (true);
        }

        private static void DrawLogedInMenu(List<Customer> allCustomers, List<Product> allProducts, Customer logedInCustomer)
        {
            do
            {
                Console.Clear();
                Console.WriteLine(p1 + $"\n                                Welcome {logedInCustomer.username}\n");
                Console.WriteLine(p2 + "Please select one of the options:             ,--./,-.");
                Console.WriteLine("                                                       /,-._.--~\\");
                Console.WriteLine(p2 + "(1) Shop luxurious fruit                      __}  {");
                Console.WriteLine(p2 + "(2) View cart                                \\`-._,-`-,");
                Console.WriteLine(p2 + "(3) Go to checkout                            `._,._,'");
                Console.WriteLine(p2 + "(4) Log out\n\n\n" + p1);
                DrawBorders(11);

                ConsoleKeyInfo cki;

                string currency = "£";

                do
                {
                    Console.SetCursorPosition(0, 13);
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.D1) { DrawStore(allCustomers, allProducts, logedInCustomer, currency); break; }
                    else if (cki.Key == ConsoleKey.D2) { DrawCart(logedInCustomer, currency); break; }
                    else if (cki.Key == ConsoleKey.D3) { DrawCheckout(logedInCustomer); break; }
                    else if (cki.Key == ConsoleKey.D4) { goto Logout; }
                    else { Console.SetCursorPosition(0, 13);  Console.Write("Please select one of the listed alternatives."); }
                } while (true);
            } while (true);
        Logout:;

        }

        private static void DrawStore(List<Customer> allCustomers, List<Product> allProducts, Customer logedInCustomer, string currency)
        {
            ConsoleKeyInfo cki;

            do
            {
                Console.Clear();
                Console.WriteLine(p1 + "                                                               (C)urrency");
                Console.WriteLine(p2 + "   The store currently offers the following products:           " + "\n");

                for (int i = 0; i < allProducts.Count; i++)
                {
                    Console.Write(p2 + "(" + (i + 1) + ") " + allProducts[i].name);
                    for (int i2 = 0; i2 < 43 - allProducts[i].name.Length; i2++)
                    {
                        Console.Write(".");
                    }
                    Console.Write($"{currency}{allProducts[i].Price(currency).ToString("N2")}\n");
                }

                Console.Write(p2 + "\n" + p2 + "(G)o Back                                     (V)iew Cart\n\n" + p1);
                Console.WriteLine("\nLogged in as: " + logedInCustomer.username + "                           Number of items in cart: " + logedInCustomer.CartCount() + "\n");
                DrawBorders(6 + allProducts.Count());

                cki = Console.ReadKey();

                if (char.IsDigit(cki.KeyChar))
                {
                    int i = int.Parse(cki.KeyChar.ToString());
                    if (i > 0 && i < (allProducts.Count + 1))
                    {
                        logedInCustomer.AddToCart(allProducts[i-1]);
                        SaveCustomers(allCustomers);
                    }
                }
                else if(cki.Key == ConsoleKey.G)
                {
                    break;
                }
                else if (cki.Key == ConsoleKey.V)
                {
                    DrawCart(logedInCustomer, currency);
                }
                else if (cki.Key == ConsoleKey.C)
                {
                    if (currency == "$") { currency = "SEK "; }
                    else if (currency == "SEK ") { currency = "£"; }
                    else if (currency == "£") { currency = "$"; }
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

                while (cartItems.Count != 0)
                {
                    printItems = cartItems.FindAll(products => products.name == cartItems[0].name);

                    Console.Write(s = (p2 + $"{printItems[0].name} (x{printItems.Count()})"));

                    for (int i = 0; i < 50 - s.Length; i++)
                    {
                        Console.Write(".");
                    }

                    Console.Write($"({currency}{printItems[0].Price(currency):N2} ea.)");
                    Console.WriteLine($"\n                                                        {printItems.Count()}x  {currency}{printItems.Count() * printItems[0].Price(currency):N2}\n");

                    totalPrice += printItems.Count() * printItems[0].Price(currency);
                    cartItems.RemoveRange(0, printItems.Count());
                }

                Console.Write($"\n                                                     Total: {currency}{totalPrice:N2}\n");

                if (logedInCustomer.GetType().Equals(typeof(BronzeCustomer)))
                {
                    Console.Write("                                             Bronze Member: -5%\n\n");
                    Console.WriteLine($"                                                 New Total: {currency}{(totalPrice * 0.95).ToString("N2")}\n");
                }
                else if (logedInCustomer.GetType().Equals(typeof(SilverCustomer)))
                {
                    Console.Write("                                             Silver Member: -10%\n\n");
                    Console.WriteLine($"                                                 New Total: {currency}{(totalPrice * 0.90).ToString("N2")}\n");
                }
                else if (logedInCustomer.GetType().Equals(typeof(GoldCustomer)))
                {
                    Console.Write("                                               Gold Member: -15%\n\n");
                    Console.WriteLine($"                                                 New Total: {currency}{(totalPrice * 0.85).ToString("N2")}\n");
                }

                Console.WriteLine("\n           (G)o Back                                 (B)uy Fruits\n\n" + p1);

                if (logedInCustomer.GetType().Equals(typeof(Customer)))
                {
                    DrawBorders(10 + 3 * logedInCustomer.cart.Distinct().Count());
                }else
                {
                    DrawBorders(13 + 3 * logedInCustomer.cart.Distinct().Count());
                }
                
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
                    DrawCheckout(logedInCustomer);
                }
                
            } while (true);
        }

        private static void DrawCheckout(Customer logedInCustomer)
        {
            ConsoleKeyInfo cki;
            int choiceNumber = 1;
           
            do
            {
                Console.Clear();
                Console.WriteLine(p1 + "\n                   Choose your payment method:\n");
                Console.WriteLine(p2 + "         Hugs");
                Console.WriteLine(p2 + "         \"Real\" Money");
                Console.WriteLine(p2 + "         Apple Seeds\n\n");
                Console.WriteLine("           (G)o Back                                   (P)urchase\n\n" + p1);
                DrawBorders(10);

                if (choiceNumber == 1)
                {
                    Console.SetCursorPosition(15, 4);
                    Console.Write("->");
                    Console.SetCursorPosition(0, 11);
                }
                else if (choiceNumber == 2)
                {
                    Console.SetCursorPosition(15, 5);
                    Console.Write("->");
                    Console.SetCursorPosition(0, 11);
                }
                else if (choiceNumber == 3)
                {
                    Console.SetCursorPosition(15, 6);
                    Console.Write("->");
                    Console.SetCursorPosition(0, 11);
                }

                cki = Console.ReadKey();

                if (cki.Key == ConsoleKey.Tab || cki.Key == ConsoleKey.DownArrow)
                {
                    choiceNumber++;
                    if (choiceNumber == 4)
                    {
                        choiceNumber = 1;
                    }
                }
                else if (cki.Key == ConsoleKey.UpArrow)
                {
                    choiceNumber--;
                    if (choiceNumber == 0)
                    {
                        choiceNumber = 3;
                    }
                }
                else if (cki.Key == ConsoleKey.G)
                {
                    break;
                }
                else if (cki.Key == ConsoleKey.P)
                {
                    Console.SetCursorPosition(0, 12);
                    Console.WriteLine("The fruits will be delivered in 2 - 58 business days. Thank you for shopping!");
                    logedInCustomer.ClearCart();
                    Console.WriteLine("Your cart has been cleared, Press any key to continue...");
                    Console.ReadKey();
                }

            } while (true);
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
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            List<Customer> customers = (List<Customer>)formatter.Deserialize(stream);
            stream.Close();

            return customers;
        }

        private static void DrawBorders(int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                Console.SetCursorPosition(0, i+1);
                Console.Write("#");
                Console.SetCursorPosition(75, i+1);
                Console.Write("#");
            }
        }

    }
}





