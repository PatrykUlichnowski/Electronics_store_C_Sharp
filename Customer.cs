using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    internal class Customer : User
    {
        //class for creating a customer type of object, this one is used only for customers cause
        //admin cannot order anything so we save a bit of memory.
        public int Id { get; set; }
        public List<Order> CustomerOrders { get; set; } = new List<Order>();
        public Customer(string login, string password, int Id) : base(login, password)
        {
            this.Id = Id;
        }

        public void ListAllCustomerOrders()
        {
            //prints all the orders of a logged user
            Console.Clear();
            int counter = 1;
            if (CustomerOrders.Count == 0)
            {
                Console.WriteLine("You don't have any orders");
            }
            else
            {
                foreach (var order in CustomerOrders)
                {
                    Console.WriteLine("Order number.\tproduct Id.\tmanufacturer name\tmanufacturer country\tmodel name\tprice");
                    Console.Write(counter + ".\t");
                    order.OrderedProduct.DispalyInfo();
                }
            }
        }
        public void AssignOrdersToCustomer(List<Order> allOrders)
        {
            //dont think this is needed in the new version i will leave it just in case
            foreach (var order in allOrders)
            {
                if (order.CustomerID == Id)
                {
                    CustomerOrders.Add(order);
                }
            }
        }
        public void AddNewOrder(Order newOrder)
        {
            //as simple as it is
            CustomerOrders.Add(newOrder);
        }
    }
}
