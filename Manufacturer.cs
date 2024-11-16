using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    abstract class Manufacturer
    {
        //just a class
        public string ManufacturerName { get; set; }
        public string Country { get; set; }

        public virtual void DispalyInfo()
        {
            Console.Write(ManufacturerName + "\t" + Country + "\t");
        }
        public abstract void ChangeManufacturer(string name, string country);
    }
}
