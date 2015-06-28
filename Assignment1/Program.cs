using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Cart> Items = new List<Cart>();
            ReadFromFile(Items);
            Console.WriteLine("WELCOME TO THE CONSOLE SHOPPING CART");            

            int Choice = 0;
            while (Choice != 5)  /* The program run continuously until user chooses 5 */
            {
                PrintMenu();
                Console.Write("\nYour choice: ");
                Choice = int.Parse(Console.ReadLine());

                if (Choice == 1)  /* Add an item */
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;  // change text color to green

                    Console.Write("Enter item name: ");
                    string Name = Console.ReadLine();
                    Console.Write("Enter available quantity: ");
                    string Quantity = Console.ReadLine();
                    Console.Write("Enter unit price ($): ");
                    string Price = Console.ReadLine();  

                    Items.Add(new Cart(Name, int.Parse(Quantity), int.Parse(Price)));
                    Console.WriteLine("A new item is added to the cart.");

                    Console.ResetColor();  // reset color back to the origin
                }
                else if (Choice == 3)  /* View all items in cart */
                {                    
                    int Length = 0;
                    foreach (Cart Item in Items)  // check for longest row to get its length
                    {
                        string Line = String.Format("{0}: {1} units, ${2}/item", Item.ItemName, Item.Quantity, Item.Price);
                        if (Line.Length > Length)
                        {
                            Length = Line.Length;
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGreen;  // change text color to green

                    Console.WriteLine("LIST OF ALL ITEMS IN THE CART\n");                    
                    foreach (Cart Item in Items)
                    {
                        Console.WriteLine(String.Format("{0}: {1} units, ${2}/item", Item.ItemName, Item.Quantity, Item.Price));
                        for (int i = 0; i < Length; i++)
                        {
                            Console.Write("-");
                        }
                        Console.WriteLine();
                    }

                    Console.ResetColor();  // reset color back to the origin
                }
                else if (Choice == 5)  /* Exit the program */
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;  // change text color to green

                    Console.WriteLine("Good bye and Thank you for using the system.");  
                    Console.Beep();  // generate beep sound                   

                    Console.ResetColor();  // reset color back to the origin
                    Thread.Sleep(1000);  // pause the program for 1 seconds
                }
            }                                                                      
        }

        public static void PrintMenu()
        {
            Console.WriteLine("\nFollowing options are available for you:\n");
            Console.WriteLine("1.  Add an item to cart");
            Console.WriteLine("2.  Remove an item from the cart");
            Console.WriteLine("3.  View the cart");
            Console.WriteLine("4.  Checkout the Pay");
            Console.WriteLine("5.  Exit");
        }

        public static void ReadFromFile(List<Cart> Items)
        {
            StreamReader reader = new StreamReader("productInventory.txt");
            while (!reader.EndOfStream)
            {
                string Item = reader.ReadLine();
                string[] ItemProp = Item.Split(',');

                string Name = null; int Quantity = 0; int Price = 0;
                for (int i = 0; i < ItemProp.Length; i++)
                {
                    if (i == 1)
                    {
                        Name = ItemProp[i];
                    }
                    else if (i == 2)
                    {
                        Quantity = int.Parse(ItemProp[i]);
                    }
                    else if (i == 3)
                    {
                        Price = int.Parse(ItemProp[i]);
                    }
                }

                Items.Add(new Cart(Name, Quantity, Price));
            }
        }
    }
}
