using BikeRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BikeRental
{
    class Program
    {
        static void Main(string[] args)
        {
            // FirstTimeStarting(); 
            // Den här metoden är till för att fylla på med data när du hämtar hem filerna. 
            // Glöm bara inte att kommentera bort den igen, eller ta bort den helt, om du skulle köra programmet fler än en gång!

            // MoreBikes();    // Lägger till en affär och 4 nya cyklar
            // Ska inte heller glömmas att kommentera eller tas bort.

            var exit = true;
            var menu = new MenuHandler(); // alla menyer
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Rent-A-Bike");
                Console.WriteLine("---------------------------");
                Console.WriteLine("Press any key to go to menu");
                Console.ReadKey();

                menu.DisplayMenu(); // Huvudmenyn
            
            var input = Console.ReadLine();  // input för huvudmenyn

                switch (input) // tar input från huvudmenyn in till respektive andrameny
                {
                    case "1":
                        menu.ViewData(); // meny för att se olika data
                        var ViewInput = Console.ReadLine(); // input för "view"menyn
                        switch (ViewInput)
                        {
                            case "1":
                                ViewBikes(); // se alla cyklar
                                break;
                            case "2":
                                ViewOrders(); // se alla ordrar
                                break;
                            case "3":
                                ViewStores(); // se alla affärer
                                break;
                            case "4":
                                ViewCustomers(); // se alla kunder
                                break;
                        }
                        break;
                    case "2":
                        menu.AddData(); // Meny med val för att lägga till data
                        var AddInput = Console.ReadLine(); // input för "add"menyn
                        switch (AddInput)
                        {
                            case "1":
                                AddBike(); // lägg till cykel
                                break;
                            case "2":
                                AddOrderAndCustomer(); // lägg till order och kund
                                break;
                            case "3":
                                AddStore(); // lägg till en ny affär
                                break;
                        }
                        break;
                    case "3":
                        menu.EditData(); // meny för att ändra på data
                        var EditInput = Console.ReadLine(); // input för "edit"menyn
                        switch (EditInput)
                        {
                            case "1":
                                EditOrder(); // metoden som ändrar på befintliga ordrar
                                break;
                        }
                        break;
                    case "4":
                        menu.DeleteFromDatabase(); // meny för att ta bort data
                        var DeleteInput = Console.ReadLine(); // input för "delete"menyn
                        switch (DeleteInput)
                        {
                            case "1":
                                DeleteBike();
                                break;
                            case "2":
                                DeleteStore();
                                break;
                            case "3":
                                DeleteCustomer();
                                break;
                            case "4":
                                DeleteOrder();
                                break;
                        }
                        break;
                    case "5":
                        exit = false; // bryter ut ur while-loopen för att stänga ner programmet.
                        break;
                } // alla olika fall för input i huvudmenyn.
            }
        }


                                                                                // Add metoder

        // Lägg till en cykel i databasen
        private static void AddBike()
        {
            Console.Clear();

            var menu = new MenuHandler(); // Gör det möjligt att kalla på ReturnToMainMenu-metoden i MenuHandler
            var bike = new Bikes();

            List<Store> Stores;// En lista över alla affärer som finns i databasen

            Console.WriteLine("What is the wheel size?");
            bike.WheelSize = int.Parse(Console.ReadLine());
            Console.WriteLine("What model is the bike?");
            bike.Model = Console.ReadLine();
            Console.WriteLine("What is the price per hour when renting?");
            bike.PricePerHour = int.Parse(Console.ReadLine());

            using (var dbContext = new BikesDbContext())
            {
                Stores = dbContext.Stores.ToList();
                foreach (var store in Stores)
                {
                    Console.WriteLine($"{store.StoreID} : {store.Location} ");
                }
                Console.WriteLine("Enter the store ID the bike belongs to");
                var StoreId = int.Parse(Console.ReadLine());
                bike.Store = Stores.Find(x => x.StoreID == StoreId);

                dbContext.Bike.Add(bike);
                dbContext.SaveChanges();

            }
        }


        // Lägg till en affär i databasen
        private static void AddStore()
        {
            var menu = new MenuHandler();
            Console.Clear();
            var store = new Store();

            Console.WriteLine("What is the location of the store?");
            store.Location = Console.ReadLine();
            using (var DbContext = new BikesDbContext())
            {
                DbContext.Stores.Add(store);
                DbContext.SaveChanges();
            }
        }
        

        // Lägg till en ny beställning av cykel och vilken kund som hyrde den
        private static void AddOrderAndCustomer()
        {
            var menu = new MenuHandler();
            var order = new Order();   // en ny order
            var cust = new Customer(); // en ny kund
            var bikeOrder = new BikesOrder();

            Console.Clear();

            // mata in information om kunden
            Console.WriteLine("What is the customers first name?");
            cust.FirstName = Console.ReadLine();
            Console.WriteLine("And the last name?");
            cust.LastName = Console.ReadLine();
            Console.WriteLine("On what phone number can we reach the customer?");
            cust.PhoneNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Type in the date of birth for the customer in '19901021' format.");
            cust.DateOfBirth = int.Parse(Console.ReadLine());
            using (var dbContext = new BikesDbContext())
            {

                dbContext.Customers.Add(cust);
                dbContext.SaveChanges(); // Sparar kunden i databasen så att customer id går att hitta för ordern.

                List<Customer> customers;
                customers = dbContext.Customers.ToList();

                foreach (var customer in customers) // Skriver ut alla kunder så man kan se alla namn och ID för att kunna mata in vilken kund som hyrde.
                {
                    Console.WriteLine($"Customer: {customer.FirstName}, ID: {customer.CustomerId}");
                }

                // mata in order info
                Console.WriteLine("What is the ID of the customer that ordered this bike?");
                var customerId = int.Parse(Console.ReadLine());
                Console.WriteLine("When was the order made? Type in '19901130' format.");
                order.Date = int.Parse(Console.ReadLine());
                Console.WriteLine("How many hours did the customer rent the bike?s");
                order.HoursRented = int.Parse(Console.ReadLine());
                
                order.Customer = customers.Find(x => x.CustomerId == customerId); // Letar efter customer ID från customerId inputen och matchar ihop.
                
                dbContext.Orders.Add(order); // lägger till order i databasen
                dbContext.SaveChanges(); // sparar ner allt till databasen

                List<Bikes> bikes;  // laddar alla cyklar
                bikes = dbContext.Bike.ToList(); // lägger alla cyklar i en list
                Console.Clear();
                foreach (var bike in bikes) // Skriver ut alla cyklar så att man kan se alla modeller och ID för att kunna mata in vilken cykel som hyrdes
                {
                    Console.WriteLine($"Bike ID {bike.BikeId}, Bike model: {bike.Model}");
                }
                Console.WriteLine("Type in the ID of the bike that was rented.");
                bikeOrder.BikeId = int.Parse(Console.ReadLine());       // cykel ID input

                List<Order> orders;
                orders = dbContext.Orders.ToList();
                foreach (var ord in orders)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"ID : {ord.OrderId}, date: {ord.Date} ");
                }
                Console.WriteLine("Type in the ID of the order that was made.");
                bikeOrder.OrderId = int.Parse(Console.ReadLine()); // hämtar in och lägger till order id i bikeOrder
                dbContext.bikesOrders.Add(bikeOrder);
                dbContext.SaveChanges();
            }
        }



                                                                                            // View Metoder


        // metod för att se varenda cykel som finns och vart dem finns någonstans
        private static void ViewBikes()
        {
            var menu = new MenuHandler();
            Console.Clear();
            List<Bikes> bikes;
            Console.WriteLine("These are all the bikes in our stores");
            using (var dbContext = new BikesDbContext())
            {
                bikes = dbContext.Bike.ToList();
                foreach (var bike in bikes)
                {
                    Console.WriteLine("");  // mellanrum i konsolen
                    Console.WriteLine($"ID: {bike.BikeId}, Model: {bike.Model}, Wheelsize: {bike.WheelSize}, Price Per Hour: {bike.PricePerHour}, store: {bike.StoreID}"); // skriver ut all info om cyklarna
                    Console.WriteLine("");  // mellanrum i konsolen
                }
            }

            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }


        // metod för att se alla affärer
        private static void ViewStores()
        {
            var menu = new MenuHandler();
            Console.Clear();
            List<Store> stores;
            Console.WriteLine("These are our current stores and their location");

            using (var dbContext = new BikesDbContext())
            {
                stores = dbContext.Stores.ToList();
                foreach (var store in stores)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"ID: {store.StoreID}, location: {store.Location}");
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }
        

        // metod för att se alla bokade cyklar och vilka som har bokat
        private static void ViewOrders()
        {
            var menu = new MenuHandler();
            Console.Clear();
            List<Order> orders;
            Console.WriteLine("These are all the previous orders made by customers");

            using (var dbContext = new BikesDbContext())
            {
                orders = dbContext.Orders.ToList();
                foreach (var order in orders)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"ID: {order.OrderId}, Customer: {order.customerId}, date: {order.Date}, total hours rented: {order.HoursRented}");
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }

        // metod för att se alla kunder som databasen har lagrat
        private static void ViewCustomers()
        {
            var menu = new MenuHandler();
            Console.Clear();

            List<Customer> customers;

            Console.WriteLine("´Here you can see all of our current and previous customers");

            using (var dbContext = new BikesDbContext())
            {
                customers = dbContext.Customers.ToList();
                foreach (var customer in customers)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"ID: {customer.CustomerId}, Phone number: {customer.PhoneNumber}, name: {customer.FirstName} {customer.LastName}, date of birth: {customer.DateOfBirth}");
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }


                                                                                            // Delete Metoder


        private static void DeleteBike() // Metod för att ta bort cyklar från databasen
        {
            var bicycle = new Bikes();
            Console.Clear();
            List<Bikes> bikes;

            using (var dbContext = new BikesDbContext())
            {
                bikes = dbContext.Bike.ToList();
                foreach (var bike in bikes)
                {
                    Console.WriteLine($"ID: {bike.BikeId}, model: {bike.Model}");
                }
                Console.WriteLine("Write in the ID of the bike that you want to remove from the database: ");
                var Input = int.Parse(Console.ReadLine());
                bicycle = dbContext.Bike.Where(b => b.BikeId == Input).First();
                dbContext.Bike.Remove(bicycle);
                dbContext.SaveChanges();
            }
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }

        private static void DeleteStore()
        {

            Console.Clear();
            List<Store> stores;
            var allStores = new Store();
            using (var dbContext = new BikesDbContext())
            {
                stores = dbContext.Stores.ToList();
                foreach (var store in stores)
                {
                    Console.WriteLine($"ID:{store.StoreID}, location: {store.Location} ");
                    Console.WriteLine();
                }
                Console.WriteLine("Type in the ID of the store you would like to remove from the database:");
                var Input = int.Parse(Console.ReadLine());
                allStores = dbContext.Stores.Where(s => s.StoreID == Input).First();
                dbContext.Stores.Remove(allStores);
                dbContext.SaveChanges();
            }

            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        } // tar bort en specifik affär med hjälp av ID


        private static void DeleteCustomer()
        {
            Console.Clear();
            List<Customer> customers;
            var allCustomers = new Customer();

            using (var dbContext = new BikesDbContext())
            {
                customers = dbContext.Customers.ToList();
                foreach (var cust in customers)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"ID : {cust.CustomerId}, name: {cust.FirstName} {cust.LastName}");
                    Console.WriteLine("");
                }

                Console.WriteLine("Type in the ID of the customer you want to delete.");
                var input = int.Parse(Console.ReadLine());

                allCustomers = dbContext.Customers.Where(c => c.CustomerId == input).First();
                dbContext.Customers.Remove(allCustomers);
                dbContext.SaveChanges();
            }
        }   // tar bort en specifik kund med hjälp av ID

        private static void DeleteOrder()
        {
            Console.Clear();
            List<Order> orders;
            var allorders = new Order();

            using (var dbContext = new BikesDbContext())
            {
                orders = dbContext.Orders.ToList();
                foreach (var order in orders)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"ID: {order.OrderId} ");
                    Console.WriteLine("");
                }
                Console.WriteLine("Type in the ID of the order you would like to delete..");
                var input = int.Parse(Console.ReadLine());

                allorders = dbContext.Orders.Where(o => o.OrderId == input).First();
                dbContext.Orders.Remove(allorders);
                dbContext.SaveChanges();
            }
        }




                                                                                                // edit metod


        private static void EditOrder()
        {
            var menu = new MenuHandler();
            var orders = new Order();
            Console.Clear();
            List<Order> allOrders;

            using (var dbContext = new BikesDbContext())
            {
                allOrders = dbContext.Orders.ToList();
                foreach (var order in allOrders)
                {
                    Console.WriteLine($"ID: {order.OrderId}, bike: {order.Bikes}, customer id: {order.customerId}, date: {order.Date}, total hours rented: {order.HoursRented}");
                    Console.WriteLine("");
                }
                Console.WriteLine("Type in the order ID of the order you would like to edit.");
                var idInput = int.Parse(Console.ReadLine());            // ID på ordern man vill ändra på
                Console.WriteLine("What in the order would you like to change?");
                Console.WriteLine("");
                Console.WriteLine("1. date");
                Console.WriteLine("2. return date");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":       // Ändrar datumet i ordern
                        Console.WriteLine("What would you like the new date to be? '19900824' format.");
                        orders = dbContext.Orders.Where(o => o.OrderId == idInput).First();
                        orders.Date = int.Parse(Console.ReadLine());
                        dbContext.Orders.Update(orders);
                        break;
                    case "2":      // ändrar antalet timmar cykeln hyrdes
                        Console.WriteLine("How many hours did the customer rent it?");
                        orders = dbContext.Orders.Where(o => o.OrderId == idInput).First();
                        orders.HoursRented = int.Parse(Console.ReadLine());
                        dbContext.Orders.Update(orders);
                        break;
                    default:
                        break;
                }

            }
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }       // Editera datumet eller returdatumet i en order. 




        private static void FirstTimeStarting() // En metod för att fylla i alla tabeller med data. Den hade nog kunnat vara väldigt mycket mer optimal men jag visste inte riktigt hur jag skulle lösa det bättre.
        {
            var bikes = new Bikes();
            var store = new Store();
            var order = new Order();
            var customer = new Customer();
            var bikesorder = new BikesOrder();
            if (true) 
            {
                using (var dbContext = new BikesDbContext())
                {
                                
                    store.Location = "Majorna";
                    dbContext.Stores.Add(store);                    // Lägger till store
                    dbContext.SaveChanges();

                    
                    bikes.Model = "Yamaha";
                    bikes.WheelSize = 26;
                    bikes.PricePerHour = 45;
                    bikes.StoreID = store.StoreID;                  // Första cykeln
                    dbContext.Bike.Add(bikes);
                    dbContext.SaveChanges();
                                        
                    customer.FirstName = "Monica";
                    customer.LastName = "Bellgarde";
                    customer.PhoneNumber = 563212;                  // första kunden
                    customer.DateOfBirth = 19810422;
                    dbContext.Customers.Add(customer);
                    dbContext.SaveChanges();
                                        
                    order.customerId = customer.CustomerId;
                    order.Date = 20190314;              
                    order.HoursRented = 4;
                    dbContext.Orders.Add(order);                    // Första orden
                    dbContext.SaveChanges();
                                        
                    bikesorder.OrderId = order.OrderId;
                    bikesorder.BikeId = bikes.BikeId;
                    dbContext.bikesOrders.Add(bikesorder);
                    dbContext.SaveChanges();

                    



                }
            }
        }

        private static void MoreBikes() // Lägger till fler cyklar och en till affär i databasen.
        {
            var store = new Store();
            var bikes = new Bikes();        // Första cykeln
            var bikes2 = new Bikes();       // Andra cykeln
            var bikes3 = new Bikes();       // Tredje cykeln
            var bikes4 = new Bikes();       // Fjärde cykeln

            using (var dbContext = new BikesDbContext()) 
            {
                store.Location = "Eriksberg";
                dbContext.Stores.Add(store);                    // Lägger till store
                dbContext.SaveChanges();

                bikes.Model = "Apollo Vaxholm";
                bikes.WheelSize = 28;
                bikes.PricePerHour = 55;
                bikes.StoreID = store.StoreID;                  
                dbContext.Bike.Add(bikes);
                dbContext.SaveChanges();

                bikes2.Model = "Specialized Sirrus X";
                bikes2.WheelSize = 30;
                bikes2.PricePerHour = 65;
                bikes2.StoreID = store.StoreID;                  
                dbContext.Bike.Add(bikes2);
                dbContext.SaveChanges();

                bikes3.Model = "Monark E-Sture";
                bikes3.WheelSize = 26;
                bikes3.PricePerHour = 85;
                bikes3.StoreID = store.StoreID;                  
                dbContext.Bike.Add(bikes3);
                dbContext.SaveChanges();

                bikes4.Model = "Crescent Knytt";
                bikes4.WheelSize = 18;
                bikes4.PricePerHour = 30;
                bikes4.StoreID = store.StoreID;                  
                dbContext.Bike.Add(bikes4);
                dbContext.SaveChanges();

            }
        }

    }
}
