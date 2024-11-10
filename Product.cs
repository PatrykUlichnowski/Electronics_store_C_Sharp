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
            this.ManufacturerName = name;
            this.Country = country;
        }
        public override void DispalyInfo()
        {
            Console.Write(this.Id + "\t");
            base.DispalyInfo();
            Console.Write(ModelName + "\t" + Price + "\n");
        }
        //Przeciążanie metod
        public void ChangeDetails(double price)
        {
            this.Price = price;
        }
        public void ChangeDetails(string modelName, double Price, string manufacturerName, string country)
        {
            this.ModelName = modelName;
            this.Country = country;
            this.ManufacturerName = manufacturerName;
            this.Price = Price;
        }
        public string DisplayPrice()
        {
            return this.Price.ToString();
        }

    }
}
