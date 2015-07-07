using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class InventoryItem : Item
    {
        public override int recordNumber { get; set; }
        public override string itemName { get; set; }
        public override int quantity { get; set; }
        public override int price { get; set; }

        public InventoryItem(int recordNumber, string itemName, int quantity, int price)
        {
            this.recordNumber = recordNumber;
            this.itemName = itemName;
            this.quantity = quantity;
            this.price = price;
        }
    }
}
