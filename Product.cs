using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Product : Manufacturer
    {
        //most complex class in this project, have a few functions 
        public int Id { get; set; }
        public string ModelName { get; set; }
        [JsonProperty] //This is required if we want to store a private variable in our JSON file
        private double Price { get; set; }

        public Product(int id, string modelName, double Price, string manufacturerName, string country)
        {
            this.Id = id;
            this.ModelName = modelName;
            this.Country = country;
            this.ManufacturerName = manufacturerName;
            this.Price = Price;
        }
        //Przesłanianie metod
        public override void ChangeManufacturer(string name, string country)
        {
            //allows to change manucafturer on a product
            this.ManufacturerName = name;
            this.Country = country;
        }
        public override void DispalyInfo()
        {
            //prints all the info about the product
            Console.Write(this.Id + "\t");
            base.DispalyInfo();
            Console.Write(ModelName + "\t" + Price + "\n");
        }
        //Przeciążanie metod
        public void ChangeDetails(double price)
        {
            //when we only want to change the price of the product
            this.Price = price;
        }
        public void ChangeDetails(string modelName, double Price, string manufacturerName, string country)
        {
            //when we want to change all the details about the product
            this.ModelName = modelName;
            this.Country = country;
            this.ManufacturerName = manufacturerName;
            this.Price = Price;
        }
        public string DisplayPrice()
        {
            //price is private so we need encapsulation
            return this.Price.ToString();
        }

    }
}
