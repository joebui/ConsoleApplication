using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    abstract class Item
    {
        public abstract int recordNumber { get; set; }
        public abstract string itemName { get; set; }
        public abstract int quantity { get; set; }
        public abstract int price { get; set; }        

        public void PrintItemDetails()
        {
            Console.WriteLine(String.Format("{0} - {1}: {2} units, ${3}/item", recordNumber, itemName, quantity, price));
        }
    }
}
