using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    abstract class Item
    {        
        public abstract string itemName { get; set; }
        public abstract int quantity { get; set; }
        public abstract int price { get; set; }

        public int CalculateTotalPrice()
        {
            return quantity * price;
        }

        public abstract void PrintItemDetails();
    }
}
