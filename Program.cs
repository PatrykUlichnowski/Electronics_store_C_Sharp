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
            string manName = "", manCountry = "", modName = "";
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
                        Console.Clear();
                        Console.WriteLine("Successfully added product to the list.\n");
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
                        Console.Clear();
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
            string manName = "", manCountry = "";
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
                    Console.Clear();
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
                        Console.Clear();
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
                Console.Clear();
                Console.WriteLine("Successfully removed item from the list.\n");
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
            Console.Clear();
            Console.WriteLine("List of all products\nId.\tmanufacturer name\tmanufacturer country\tmodel name\tprice");
            foreach (Product product in products)
            {
                product.DispalyInfo();
            }
            Console.WriteLine();
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
        static (bool loggedIn, Customer user) RegisterNewUser(List<Customer> users, bool loggedIn)
        {
            string registerLogin = "";
            string registerPasswd = "";
            bool validRegisterData = false;
            while (!validRegisterData) 
            { 
                Console.WriteLine("Your login: ");
                registerLogin = Console.ReadLine();
                Console.WriteLine("Your password: ");
                registerPasswd = Console.ReadLine();
                bool loginAlreadyExist = false;
                foreach (User user in users) 
                {
                    if (user.Login == registerLogin)
                    {
                        loginAlreadyExist = true;
                    }
                }
                if (registerLogin == "" || registerPasswd == "")
                {
                    Console.WriteLine("INVALID DATA - login and password can't be empty");
                }
                else if (loginAlreadyExist) 
                {
                    Console.WriteLine("This login already exists, please choose another one.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Your account has been successfully created!");
                    validRegisterData = true;
                    loggedIn = true;
                }
            }
            int registerUserID = Convert.ToInt32(users[users.Count - 1].Id) + 1;
            Customer newUser = new Customer(registerLogin, registerPasswd, registerUserID);
            users.Add(newUser);
            return (loggedIn, newUser);
        }
        static (bool loggedIn, bool loggedAsAdmin, Customer loggedAs) LogIn(List<Customer> users, bool loggedIn, bool loggedAsAdmin)
        {
            //we need to return multiple variables thats why there is a tuple 
            string login = "";
            string passwd = "";
            Customer whoIsLoggedIn = null;
            bool validLogInData = false;
            while (!validLogInData)
            {
                Console.WriteLine("Your login: ");
                login = Console.ReadLine();
                Console.WriteLine("Your password: ");
                passwd = Console.ReadLine();
                if (login == "" || passwd == "")
                {
                    Console.WriteLine("Login and password can't be empty, please try again.");
                }
                else if (login == "admin" && passwd == "admin")
                {
                    loggedAsAdmin = true;
                    validLogInData = true;
                    loggedIn = true;
                    Console.Clear();
                    Console.WriteLine("Successfully loged in as " + login);
                    break;
                }
                else
                {
                    bool validInfo = false;
                    foreach (Customer user in users) 
                    { 
                        if (user.Login == login && user.Password == passwd)
                        {
                            validInfo = true;
                            loggedIn = true;
                            validLogInData = true;
                            whoIsLoggedIn = user;
                            Console.Clear();
                            Console.WriteLine("Successfully loged in as " + login);
                            break;
                        }
                    }
                    if (!validInfo) 
                    {
                        Console.WriteLine("INVALID DATA");
                    }
                }
            }
            return (loggedIn, loggedAsAdmin, whoIsLoggedIn);
        }
        static void OrderNewProduct(Customer customer, List<Product> products)
        {
            ListAllProducts(products);
            int idToOrder = 1;
            Console.WriteLine("Which product would you like to order? (type in its ID)");
            idToOrder = Convert.ToInt32(Console.ReadLine());
            if (idToOrder > 0 && idToOrder <= products.Count) 
            { 
                customer.AddNewOrder(new Order(customer.Id, products[idToOrder - 1]));
            }
            else
            {
                Console.WriteLine("INVALID ID");
            }
        }
        static void Main(string[] args)
        {
            //parsing the data from a JSON file
            string productsJSONPath = "products.json";
            string productsJSONData = File.ReadAllText(productsJSONPath);
            //this was a bit of a problem so it had to be done this way
            //otherwise if JSON file was empty the app would crash
            List<Product> products = string.IsNullOrEmpty(productsJSONData) ? 
                new List<Product>() : 
                JsonConvert.DeserializeObject<List<Product>>(productsJSONData);

            
            string customersJSONPath = "customers.json";
            string customersJSONData = File.ReadAllText(customersJSONPath);
            List<Customer> customers = string.IsNullOrEmpty(customersJSONData) ?
                new List<Customer>() :
                JsonConvert.DeserializeObject<List<Customer>>(customersJSONData);
            List<Order> orders = new List<Order>();

            foreach (Customer customer in customers)
            {
                customer.AssignOrdersToCustomer(orders);
            }

            bool loggedIn = false;
            bool loggedAsAdmin = false;
            bool appRunning = true;
            Customer customerUser = null;
            
            while (!loggedIn) 
            { 
                string loginOrRegister = "";
                Console.WriteLine("Do you want to log in or register? (type in 1 or 2)\n" +
                    "1. Login\n" +
                    "2. Register");
                loginOrRegister = Console.ReadLine();
                switch (loginOrRegister)
                {
                    case "1":
                        var returnFromLogIn = LogIn(customers, loggedIn, loggedAsAdmin);
                        loggedIn = returnFromLogIn.Item1;
                        loggedAsAdmin = returnFromLogIn.Item2;
                        customerUser = returnFromLogIn.Item3;
                        break;
                    case "2":
                        var registerReturn = RegisterNewUser(customers, loggedIn);
                        loggedIn = registerReturn.Item1;
                        customerUser = registerReturn.Item2;
                        string updatedCustomersData = JsonConvert.SerializeObject(customers, Formatting.Indented);
                        File.WriteAllText(customersJSONPath, updatedCustomersData);
                        customers = string.IsNullOrEmpty(customersJSONData) ?
                            new List<Customer>() :
                            JsonConvert.DeserializeObject<List<Customer>>(customersJSONData);
                        break;
                    default:
                        Console.WriteLine("INVALID INPUT");
                        break;
                }
            }

            if (loggedAsAdmin) 
            { 
                while (appRunning)
                {
                    Console.WriteLine("MAIN ADMIN PANEL\n");
                    //Main admin menu
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
            else
            {
                while (appRunning)
                {
                    Console.WriteLine("Customer panel\n");
                    //Main admin menu
                    Console.WriteLine("Choose an option:\n" +
                        "1. List all products\n" +
                        "2. Order a new product\n" +
                        "3. List all my orders\n" +
                        "4. Quit"
                        );

                    string choosedOption = Console.ReadLine();
                    switch (choosedOption)
                    {
                        case "1":
                            ListAllProducts(products);
                            break;
                        case "2":
                            OrderNewProduct(customerUser, products);
                            break;
                        case "3":
                            customerUser.ListAllCustomerOrders();
                            break;
                        case "4":
                            appRunning = false;
                            string updatedCustomersData = JsonConvert.SerializeObject(customers, Formatting.Indented);
                            File.WriteAllText(customersJSONPath, updatedCustomersData);
                            break;
                        default:
                            Console.WriteLine("INVALID INPUT\nPlease select one of the listed options.\n\n");
                            break;
                    }
                }
            }
        }
    }
}
