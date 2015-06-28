using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Assignment1
{
    class CartItem
    {
        public int RecordNumber { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public CartItem(int RecordNumber, string ItemName, int Quantity, int Price)
        {
            this.RecordNumber = RecordNumber;
            this.ItemName = ItemName;
            this.Quantity = Quantity;
            this.Price = Price;
        }

        public int CalculateTotalPrice()
        {
            return Quantity * Price;
        }
    }
}
