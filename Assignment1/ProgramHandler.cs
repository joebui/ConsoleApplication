using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Assignment1
{
    interface ProgramHandler
    {
        void PrintMenu();        

        void ReadFromFile(List<InventoryItem> items);

        void AddAnItem(List<CartItem> items, List<InventoryItem> inventory);

        void RemoveAnItem(List<CartItem> items);

        void ViewCart(List<CartItem> items);

        void CheckOut(List<CartItem> items, List<InventoryItem> inventory);

        void ExitProgram(List<InventoryItem> inventory);
    }
}
