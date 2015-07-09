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
                try
                {
                    Console.Write("\nYour choice (1, 2, 3, 4 or 5): ");
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
                        RemoveAnItem(items);
                        break;
                    // View all items in cart.
                    case 3:
                        ViewCart(items);
                        break;
                    // Checkout and pay all the items in cart.
                    case 4:
                        CheckOut(items, inventory);
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

        static void PrintMenu()
        {
            Console.WriteLine("\nFollowing options are available for you:\n");
            Console.WriteLine("1.  Add an item to cart");
            Console.WriteLine("2.  Remove an item from the cart");
            Console.WriteLine("3.  View the cart");
            Console.WriteLine("4.  Checkout the Pay");
            Console.WriteLine("5.  Exit");
        }

        static void ReadFromFile(List<InventoryItem> items)
        {
            var reader = new StreamReader("productInventory.txt");
            while (!reader.EndOfStream)
            {
                string item = reader.ReadLine();
                // Split the string of each line of the text file by basing on ","
                // character to retrieve information of each item.
                string[] itemProp = item.Split(',');                                               

                // Add new item to the List.
                items.Add(new InventoryItem(int.Parse(itemProp[0]), itemProp[1], int.Parse(itemProp[2]), int.Parse(itemProp[3])));
            }

            reader.Close();
        }

        static void AddAnItem(List<CartItem> items, List<InventoryItem> inventory)
        {
            Console.WriteLine("These are the available items in the inventory:\n");            
            // View all the items in the inventory.            
            foreach (var item in inventory)
            {
                item.PrintItemDetails();
                for (var i = 0; i < 20; i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
            }
            
            // The process repeats until user enters valid inputs.
            while (true)
            {                               
                // Check if user has entered invalid inputs.
                try
                {
                    // Get user input about the item's properties.
                    Console.Write("Enter item id: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Enter quantity of the item: ");
                    string quantity = Console.ReadLine();
                    
                    var quantityNumber = int.Parse(quantity);                                                            
                    
                    // Search for the item in the inventory by the ID.
                    var requiredItems = from item in inventory
                                       where item.recordNumber == id
                                       select new CartItem(item.recordNumber, item.itemName, item.quantity, item.price);

                    // Check if the entered id is available in the inventory.
                    if (!requiredItems.Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You can't add this item as it isn't available in the inventory.");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {                        
                        // Compare the entered quantity with that in the inventory.
                        var requiredItemNumbers = from item in requiredItems
                                                    where (item.quantity >= quantityNumber) && (quantityNumber > 0)
                                                    select new CartItem(item.recordNumber, item.itemName, quantityNumber, item.price);

                        if (!requiredItemNumbers.Any())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The quantity must be larger than 0 AND must not exceed the quantity\navailable in the inventory.");
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            foreach (var item in requiredItemNumbers)
                            {
                                // Add new item to cart.
                                items.Add(item);
                                // Update the stock value in the inventory.
                                InventoryItem result = inventory.Find(resultItem => resultItem.recordNumber == item.recordNumber);
                                result.quantity -= quantityNumber;                                
                                Console.WriteLine("\nA new item is added to the cart.");
                            }
                        }                                                                           
                    }

                    break;                
                }
                catch (Exception e)
                {
                    // Change text color to red.
                    Console.ForegroundColor = ConsoleColor.Red;  
                    Console.WriteLine("ERROR!!!! {0} Try again.", e.Message);                                        
                    // Change text color to green.
                    Console.ForegroundColor = ConsoleColor.Green;  
                }
            }                                    
        }

        static void RemoveAnItem(List<CartItem> items)
        {                        
            while (true)
            {
                try
                {
                    Console.Write("Enter the item id to be removed: ");
                    int id = int.Parse(Console.ReadLine());
                    
                    var requiredItems = from item in items
                                       where item.recordNumber == id
                                       select new CartItem(item.recordNumber, item.itemName, item.quantity, item.price);

                    // Check if the entered item's name is available.
                    if (!requiredItems.Any())
                    {
                        // Change text color to red.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You can't remove the item as it isn't available in the category.");
                        // Change text color to green.
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        // Remove the required item from the cart.
                        List<CartItem> toList = requiredItems.ToList();                        
                        foreach (var item in toList)
                        {                            
                            items.Remove(items.Find(reqItem => reqItem.recordNumber == item.recordNumber));
                        }                        
                        Console.WriteLine("\nThe item is successfully removed from cart");
                    }

                    break;
                }
                catch (Exception e)
                {
                    // Change text color to red.
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR!!!! {0} Try again.", e.Message);
                    // Change text color to green.
                    Console.ForegroundColor = ConsoleColor.Green;
                }  
            }                                           
        }

        static void ViewCart(List<CartItem> items)
        {            
            // View the items on the console.
            Console.WriteLine("LIST OF ALL ITEMS IN YOUR CART\n");
            foreach (var item in items)
            {
                item.PrintItemDetails();                
                Console.WriteLine("--------------------");                                
            }
        }

        static void CheckOut(List<CartItem> items, List<InventoryItem> inventory)
        {            
            ViewCart(items);

            // Calculate the total price of all items in cart.
            var totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += item.TotalPrice();
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("\nTOTAL PRICE: $");
            builder.Append(totalPrice);
            Console.WriteLine(builder.ToString());            

            // Check if user wants to purchase the items.
            string answer = "";
            while ((answer != "y") && (answer != "n"))
            {
                Console.Write("Do you want to purchase all these items in your cart? (y/n): ");
                 answer = Console.ReadLine();

                switch (answer)
                {
                    case "y":
                        // Update the text file.
                        var writer = new StreamWriter("productInventory.txt");

                        foreach (var item in inventory)
                        {
                            writer.WriteLine("{0},{1},{2},{3}", item.recordNumber, item.itemName, item.quantity, item.price);
                        }
                        writer.Close();

                        Console.WriteLine("You have successfully purchased all the items in cart.");
                        items.Clear();
                        break;
                    case "n":
                        Console.WriteLine("You stop purchasing the items in cart");
                        break;
                    default:
                        // Change text color to red.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR!!! Wrong input, you must type \"y\" or \"n\".");                        
                        // Change text color to green.
                        Console.ForegroundColor = ConsoleColor.Green;                          
                        break;
                }
            }
        }

        static void ExitProgram(List<InventoryItem> inventory)
        {            
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
