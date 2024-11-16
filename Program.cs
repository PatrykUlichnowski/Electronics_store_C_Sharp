using static System.Net.Mime.MediaTypeNames;
using System;
using Newtonsoft.Json;

namespace Projekt
{
    internal class Program
    {
        static void AddNewProduct(List<Product> products)
        {
            //Allows the admin to insert a new product at the end of the List of products.
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
            //Takes the original list as a parameter, and then changes the value of desired 
            //fields to the one choosed by administrator. We identify the correct item
            //in the list by idToEdit
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
            //Less detailed version of EditProductDetails, this one allows the admin
            //to change only manufacturer of choosed product
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
            //Less detailed version of EditProductDetails, this one allows admin to change only the price of the product
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
            //This function works as sort of interface to all the other Edit functions, its sort of like menu
            //in which the admin just chooses which operation he likes to
            ListAllProducts(products);
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
            //This function alows the admin to remove selected item from the products list
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
            //This function just prints all the products stored in the list
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
             * it goes through each one in the list
             * and assigns it a new ID, otherwise the user will see a product 
             * with a index of f.e. 2, then when their type in ID 2 to the remove function
             * it wont be able to remove it, because even tho the displayed ID is 2
             * the real index in the list of that product is now set to 1
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
            //Alows new customers to create a new account. It have to return a tuple with status of being logged in,
            //and a new Customer item that we add to the customers list (for some reason this doesnt work otherwise,
            //while when adding a new product we dont have to return the object itself).
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
            int registerUserID = users.Count;
            Customer newUser = new Customer(registerLogin, registerPasswd, registerUserID);
            return (loggedIn, newUser);
        }
        static (bool loggedIn, bool loggedAsAdmin, Customer loggedAs) LogIn(List<Customer> users, bool loggedIn, bool loggedAsAdmin)
        {
            //In here we check whether the user wants to log as a administrator or customer.
            //We need to return the loggedAs object so that the program can be adjust to whoever uses it.
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
                            Console.WriteLine("Successfully logged in as " + login);
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
            //This function is available only to the customers and allows them to order a new product
            //which means it will be added at the end of their orders list
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
                //i dont think this is needed cause its a left over from the older version that used customers.json file
                //but i will leave it in here just in case
                customer.AssignOrdersToCustomer(orders);
            }

            bool loggedIn = false;
            bool loggedAsAdmin = false;
            bool appRunning = true;
            Customer customerUser = null;
            
            while (!loggedIn) 
            { 
                //login loop - this assures that the program doesnt continue until a user is logged in
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
                        customers.Add(customerUser);
                        //string updatedCustomersData = JsonConvert.SerializeObject(customers, Formatting.Indented);
                        //File.WriteAllText(customersJSONPath, updatedCustomersData);
                        //customers = string.IsNullOrEmpty(customersJSONData) ?
                        //    new List<Customer>() :
                        //    JsonConvert.DeserializeObject<List<Customer>>(customersJSONData);
                        break;
                    default:
                        Console.WriteLine("INVALID INPUT");
                        break;
                }
            }

            if (loggedAsAdmin) 
            { 
                //Main program loop for the admin version
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
                //Main program loop for the customer version
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
