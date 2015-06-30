using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class InventoryItem
    {
        public int recordNumber { get; set; }
        public string itemName { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }

        public InventoryItem(int recordNumber, string itemName, int quantity, int price)
        {
            this.recordNumber = recordNumber;
            this.itemName = itemName;
            this.quantity = quantity;
            this.price = price;
        }

        public void PrintItemDetails()
        {
            Console.WriteLine(String.Format("{0} - {1}: {2} units, ${3}/item", recordNumber, itemName, quantity, price));
        }
    }
}
