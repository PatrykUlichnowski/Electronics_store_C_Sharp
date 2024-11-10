using static System.Net.Mime.MediaTypeNames;
using System;
using Newtonsoft.Json;

namespace Projekt
{
    internal class Program
    {
        static void AddNewProduct(List<Product> products)
        {
            //Allows the user to inser a new product at the end of the List of products.
            bool validStrings = false;
            string manName = null, manCountry = null, modName = null;
            while (!validStrings)
            {
                //Checks whether the strings arent empty (they could be empty,
                //it just looks better when they arent)
                Console.WriteLine("Please fill the required data: ");
                Console.WriteLine("Manufacturer name: ");
                manName = Console.ReadLine();
                Console.WriteLine("Manufacturer country: ");
                manCountry = Console.ReadLine();
                Console.WriteLine("Model name: ");
                modName = Console.ReadLine();
                if (manCountry == "" || manName == "" || modName == "") 
                {
                    Console.WriteLine("INVALID DATA, at least one of the inputs is empty.\n");
                }
                else
                {
                    validStrings = true;
                }
            }
            bool validPrice = false;
            while (!validPrice)
            {
                Console.WriteLine("Price: ");
                double price = 0;
                string txtprice = Console.ReadLine();
                //This was stolen from Stackoverflow, it checks if it can convert the txtprice to double
                //type and if it can then it will return the double value to the price variable
                if (Double.TryParse(txtprice, out price))
                {
                    if (price >= 0)
                    {
                        validPrice = true;
                        if (products.Count > 0)
                        {
                            //Here we just get the last id in the list and increment it
                            products.Add(new Product(products.Last().Id + 1, modName, price, manName, manCountry));
                        }
                        else
                        {
                            //Here is when the list is empty
                            products.Add(new Product(1, modName, price, manName, manCountry));
                        }
                        Console.WriteLine("Successfully added product to the list.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("INVALID VALUE OF PRICE, must be some sort of numeric value.");
                }
            }
        }
        static void EditProductDetails(List<Product> products, int idToEdit)
        {
            bool validStrings = false;
            string manName = null, manCountry = null, modName = null;
            while (!validStrings)
            {
                Console.WriteLine("Please fill the required data: ");
                Console.WriteLine("Manufacturer name: ");
                manName = Console.ReadLine();
                Console.WriteLine("Manufacturer country: ");
                manCountry = Console.ReadLine();
                Console.WriteLine("Model name: ");
                modName = Console.ReadLine();

                if (manCountry == "" || manName == "" || modName == "")
                {
                    Console.WriteLine("INVALID DATA, at least one of the inputs is empty.\n");
                }
                else
                {
                    validStrings = true;
                }
            }
            bool validPrice = false;
            while (!validPrice)
            {
                Console.WriteLine("Price: ");
                double price = 0;
                string txtprice = Console.ReadLine();

                if (Double.TryParse(txtprice, out price))
                {
                    if (price >= 0)
                    {
                        //Here instead of adding new element at the end we just modify the existing one
                        //with specified ID
                        validPrice = true;
                        products[idToEdit].ChangeDetails(modName, price, manName, manCountry);
                        Console.WriteLine("Successfully edited existing product.\n");
                    }
                }
                else
                {
                    Console.WriteLine("INVALID VALUE OF PRICE, must be some sort of numeric value.");
                }
            }
        }
        static void EditProductManufaturer(List<Product> products, int idToEdit)
        {
            bool validStrings = false;
            string manName = null, manCountry = null;
            while (!validStrings)
            {
                Console.WriteLine("Please fill the required data: ");
                Console.WriteLine("Manufacturer name: ");
                manName = Console.ReadLine();
                Console.WriteLine("Manufacturer country: ");
                manCountry = Console.ReadLine();

                if (manCountry == "" || manName == "")
                {
                    Console.WriteLine("INVALID DATA, at least one of the inputs is empty.\n");
                }
                else
                {
                    validStrings = true;
                    products[idToEdit].ChangeManufacturer(manName, manCountry);
                    Console.WriteLine("Successfully edited existing product.\n");
                }
            }
        }
        static void EditProductPrice(List<Product> products, int idToEdit)
        {
            Console.WriteLine("Please fill the required data: ");
            bool validPrice = false;
            while (!validPrice)
            {
                Console.WriteLine("Price: ");
                double price = 0;
                string txtprice = Console.ReadLine();

                if (Double.TryParse(txtprice, out price))
                {
                    if (price >= 0)
                    {
                        validPrice = true;
                        products[idToEdit].ChangeDetails(price);
                        Console.WriteLine("Successfully edited existing product.\n");
                    }
                }
                else
                {
                    Console.WriteLine("INVALID VALUE OF PRICE, must be some sort of numeric value.");
                }
            }
        }
        static void EditExistingProduct(List<Product> products)
        {
            ListAllProducts(products);
            //This works almost the same way AddNewProduct does with a small change after setting the price
            Console.WriteLine("To edit an exisitng product please select its Id:");
            int idToEdit = Convert.ToInt32(Console.ReadLine());
            idToEdit--;
            bool idValid = false;

            if (idToEdit <= products.Count)
            {
                idValid = true;
            }

            if (!idValid)
            {
                Console.WriteLine("INVALID ID.");
            }
            else
            {
                Console.WriteLine("Do you want to change:\n" +
                    "a) Manufacturer inforation\n" +
                    "b) Price\n" +
                    "c) All details");
                string whatToChange = Console.ReadLine();
                switch (whatToChange)
                {
                    case "a":
                        EditProductManufaturer(products, idToEdit);
                        break;
                    case "b":
                        EditProductPrice(products, idToEdit);
                        break;
                    case "c":
                        EditProductDetails(products, idToEdit);
                        break;
                    default:
                        Console.WriteLine("INVALID CHOICE");
                        break;
                }
            }
        }
        static void RemoveExistingProduct(List<Product> products)
        {
            ListAllProducts(products);
            Console.WriteLine("Please select Id of a product that you want to remove: ");
            int idToRemove = Convert.ToInt32(Console.ReadLine());
            //Checks if the provided ID even exists
            bool idValid = false;
            if (idToRemove <= products.Count)
            {
                idValid = true;
            }
            if (idValid) 
            {
                //We need to decrease the ID by one cause the list stores value 0..n while our program
                //displays is as 1..n for the better human readable experience
                products.RemoveAt(idToRemove - 1);
                //See the explanation in the function itself
                RefreshIds(products);
            }
            else
            {
                Console.WriteLine("INVALID ID.");
            }
        }
        static void ListAllProducts(List<Product> products)
        {
            Console.WriteLine("List of all products\nId.\tmanufacturer name\tmanufacturer country\tmodel name\tprice");
            foreach (Product product in products)
            {
                product.DispalyInfo();
            }
        }
        static void RefreshIds(List<Product> products)
        {
            /*This fucntion is crucial for removing products - after one of the products get deleted
             * it goes through each one
             * and assigns it a new ID because otherwise the user will see a product 
             * with a index of f.e. 2, then when their type in ID 2 to the remove function
             * it wont be able to remove it, because even tho the ID is 2
             * the real index of that product in the list is now 1
             * and since we are using .RemoveAt(), the indexes have to match
             */
            int newIdCounter = 1;
            foreach (Product product in products)
            {
                product.Id = newIdCounter;
                newIdCounter++;
            }
        }
        
        static void Main(string[] args)
        {
            string productsJSONPath = "products.json";
            string productsJSONData = File.ReadAllText(productsJSONPath);
            //parsing the data from a JSON file
            List<Product> products = string.IsNullOrEmpty(productsJSONData) ? 
                new List<Product>() : 
                JsonConvert.DeserializeObject<List<Product>>(productsJSONData);

            Console.WriteLine("MAIN PANEL\n");
            bool appRunning = true;
            while (appRunning)
            {
                //Main menu
                Console.WriteLine("Choose an option:\n" +
                    "1. Add new product\n" +
                    "2. Edit existing product\n" +
                    "3. Remove existing product\n" +
                    "4. List all products\n" +
                    "5. Quit"
                    );

                string choosedOption = Console.ReadLine();
                switch (choosedOption)
                {
                    case "1":
                        AddNewProduct(products);
                        break;
                    case "2":
                        EditExistingProduct(products);
                        break;
                    case "3":
                        RemoveExistingProduct(products);
                        break;
                    case "4":
                        ListAllProducts(products);
                        break;
                    case "5":
                        //writes the data into a json file
                        string updatedProductsData = JsonConvert.SerializeObject(products, Formatting.Indented);
                        File.WriteAllText(productsJSONPath, updatedProductsData);
                        appRunning = false;
                        break;
                    default:
                        Console.WriteLine("INVALID INPUT\nPlease select one of the listed options.\n\n");
                        break;
                }
            }
        }
    }
}
