using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    internal class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }
    }
    internal class Customer : User
    {
        public int Id { get; set; }
        public List<Order> CustomerOrders { get; set; } = new List<Order>();
        //dziedziczenie konstruktora
        public Customer(string login, string password, int Id) : base(login, password)
        {
            this.Id = Id;
        }
        
        public void ListAllCustomerOrders()
        {
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
            CustomerOrders.Add(newOrder);
        }
    }
}
