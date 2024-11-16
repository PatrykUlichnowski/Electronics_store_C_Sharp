using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    internal class Order
    {
        // just an order class, very simple
        public int CustomerID { get; set; }
        public Product OrderedProduct { get; set; }
        public Order(int customerID, Product orderedProduct)
        {
            this.CustomerID = customerID;
            this.OrderedProduct = orderedProduct;
        }
    }
}
