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
            var items = new List<CartItem>();
            var inventory = new List<InventoryItem>();
            ReadFromFile(inventory);
            Console.WriteLine("WELCOME TO THE CONSOLE SHOPPING CART");            

            var choice = 0;
            // This loop will run the program run continuously until user chooses 5.
            while (choice != 5)  
            {
                PrintMenu();
                Console.Write("\nYour choice (1, 2, 3, 4 or 5): ");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    // Change text color to red.
                    Console.ForegroundColor = ConsoleColor.Red;  
                    Console.WriteLine("ERROR!!!! {0} Try again.", e.Message);
                    // Reset color back to the origin.
                    Console.ResetColor();  
                    continue;
                }

                // Change text color to green.
                Console.ForegroundColor = ConsoleColor.Green;                
                switch (choice)
                {
                    // Add an item to cart.
                    case 1:
                        AddAnItem(items, inventory);
                        break;
                    // Remove an item to cart.
                    case 2:
                        RemoveAnItem(items, inventory);
                        break;
                    // View all items in cart.
                    case 3:
                        ViewCart(items);
                        break;
                    // Checkout and pay all the items in cart.
                    case 4:
                        CheckOut(items);
                        break;
                    // Exit the program.
                    case 5:
                        ExitProgram(inventory);
                        break;
                    default:
                        // Change text color to red.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR!!!! This option is not available. Try again");
                        // Change text color to green.
                        Console.ForegroundColor = ConsoleColor.Green;  
                        break;
                }                
                // Reset color back to the origin.
                Console.ResetColor();  
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

        public static void ReadFromFile(List<InventoryItem> items)
        {
            var reader = new StreamReader("productInventory.txt");
            while (!reader.EndOfStream)
            {
                string item = reader.ReadLine();
                string[] itemProp = item.Split(',');

                var name = ""; var quantity = 0;
                var price = 0; var recordNumber = 0;

                // Split the string of each line of the text file by basing on ","
                // character to retrieve information of each item.
                for (var i = 0; i < itemProp.Length; i++)
                {
                    if (i == 0)
                    {
                        recordNumber = int.Parse(itemProp[i]);
                    }
                    else if (i == 1)
                    {
                        name = itemProp[i];
                    }
                    else if (i == 2)
                    {
                        quantity = int.Parse(itemProp[i]);
                    }
                    else
                    {
                        price = int.Parse(itemProp[i]);
                    }
                }

                // Add new item to the List.
                items.Add(new InventoryItem(recordNumber, name, quantity, price));
            }

            reader.Close();
        }

        public static void AddAnItem(List<CartItem> items, List<InventoryItem> inventory)
        {
            Console.WriteLine("These are the available items in the inventory.\n");            
            // View the items in the inventory.            
            foreach (var item in inventory)
            {
                item.PrintItemDetails();
                for (var i = 0; i < 20; i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
            }

            var isFailed = true;  
            while (isFailed == true)
            {
                // Get user input about the item's properties.
                Console.Write("\nEnter item name: ");
                string name = Console.ReadLine();
                Console.Write("Enter quantity of the item: ");
                string quantity = Console.ReadLine();                

                // Check if user has entered invalid inputs.
                try
                {
                    var quantityNumber = int.Parse(quantity);
                    // The inputs are valid
                    isFailed = false;

                    var isAvailable = false;
                    foreach (var item in inventory)
                    {
                        if (item.itemName == name)
                        {
                            if (item.quantity >= quantityNumber)
                            {
                                items.Add(new CartItem(name, int.Parse(quantity), item.price));
                                item.quantity -= quantityNumber;
                                Console.WriteLine("\nA new item is added to the cart.");                                
                            }
                            else
                            {
                                Console.WriteLine("\nThe quantity to order exceeds the quantity available in the stock.");
                            }

                            isAvailable = true;
                            break;
                        }
                    }

                    if (isAvailable == false)
                    {
                        Console.WriteLine("\nThe item's name is not available in the inventory.");
                    }                     
                }
                catch (Exception e)
                {
                    // Change text color to red.
                    Console.ForegroundColor = ConsoleColor.Red;  
                    Console.WriteLine("ERROR!!!! {0} Try again.", e.Message);
                    // The inputs are invalid.
                    isFailed = true;
                    // Change text color to green.
                    Console.ForegroundColor = ConsoleColor.Green;  
                }
            }                                    
        }

        public static void RemoveAnItem(List<CartItem> items, List<InventoryItem> inventory)
        {            
            var isFailed = true;
            while (isFailed == true)
            {
                Console.Write("\nEnter the item name to be removed: ");
                string itemName = Console.ReadLine();
                // Check if the entered item's name is available.
                foreach (var item in items)
                {
                    // The item's name is available.
                    if (item.itemName == itemName)
                    {
                        items.Remove(item);                        
                        isFailed = false;
                        break;
                    }
                }

                // The item's name is unavailable.
                if (isFailed == true)
                {
                    // Change text color to red.
                    Console.ForegroundColor = ConsoleColor.Red;  
                    Console.WriteLine("ERROR!!!! The item name is not available in the cart. Try again");
                    // Change text color to green.
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }           

            Console.WriteLine("\nThe item is successfully removed from cart");
        }

        public static void ViewCart(List<CartItem> items)
        {            
            // View the items on the console.
            Console.WriteLine("LIST OF ALL ITEMS IN YOUR CART\n");
            foreach (var item in items)
            {
                item.PrintItemDetails();
                for (var i = 0; i < 20; i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
            }
        }

        public static void CheckOut(List<CartItem> items)
        {            
            ViewCart(items);

            var totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += item.CalculateTotalPrice();
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("\nTotal: $");
            builder.Append(totalPrice);
            Console.WriteLine(builder.ToString());            

            string answer = "";
            while ((answer != "y") && (answer != "n"))
            {
                Console.Write("Do you want to purchase all these items in your cart? (y/n): ");
                 answer = Console.ReadLine();

                switch (answer)
                {
                    case "y":
                        Console.WriteLine("You have successfully purchased all the items in cart.");
                        items.Clear();
                        break;
                    case "n":
                        Console.WriteLine("You stop purchasing the items in cart");
                        break;
                    default:
                        Console.WriteLine("ERROR!!! Wrong input, you must type \"y\" or \"n\".");
                        break;
                }
            }
        }

        public static void ExitProgram(List<InventoryItem> inventory)
        {
            // Update the text file.
            var writer = new StreamWriter("productInventory.txt");
            
            foreach (var item in inventory)
            {
                writer.WriteLine("{0},{1},{2},{3}", item.recordNumber, item.itemName, item.quantity, item.price);                
            }
            writer.Close();

            Console.WriteLine("Good bye and Thank you for using the Console Shopping Cart.");
            // Generate beep sound.
            Console.Beep();

            // Reset color back to its origin.
            Console.ResetColor();
            // Pause the program for 1 seconds
            // before exit.
            Thread.Sleep(1000);  
        }
    }
}
