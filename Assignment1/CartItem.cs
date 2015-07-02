using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Assignment1
{
    class CartItem : Item
    {
        public override string itemName { get; set; }
        public override int quantity { get; set; }
        public override int price { get; set; }

        public CartItem(string itemName, int quantity, int price)
        {            
            this.itemName = itemName;
            this.quantity = quantity;
            this.price = price;
        }

        public override void PrintItemDetails()
        {
            Console.WriteLine(String.Format("{0}: {1} units, ${2}/item", itemName, quantity, price));
        }
    }
}
