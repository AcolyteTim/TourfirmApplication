using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml.Linq;

namespace TourfirmApplication.Model
{
    internal static class DataWorker
    {
        /// <summary>
        /// Creation of new Address object (row)
        /// </summary>
        /// <param name="locality">City/Town/Village name</param>
        /// <param name="street">Street/Square name</param>
        /// <param name="building">Number of building</param>
        /// <returns>Information about the success of the operation</returns>
        public static String CreateAddress(string locality, string street, string building)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking the repetitions
                    bool isExist = db.Address_.Any(el => el.a_locality == locality && el.a_street == street && el.a_building == building);

                    if (!isExist)
                    {
                        // Creating a new Address
                        Address_ newAddress = new Address_ { a_locality = locality, a_street = street, a_building = building };
                        db.Address_.Add(newAddress);
                        db.SaveChanges();
                        return "New address has been added!";
                    }
                    else
                    {   // if there is a repetition
                        return "This address has already been added!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Deleting of Address object (row).
        /// </summary>
        /// <param name="ID">Indentification decryption of Address object (row)</param>
        /// <returns>Information about the success of the operation</returns>
        public static String DeleteAddress(int ID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Address_.Any(el => el.a_id == ID);

                    if (isExist)
                    {
                        // Deleting
                        db.Address_.Remove(db.Address_.SingleOrDefault(x => x.a_id == ID));
                        db.SaveChanges();
                        return "Address has been deleted!";
                    }
                    else
                    {   // if there no such item
                        return "There is no item with this id!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Editing of Address object (row)
        /// </summary>
        /// <param name="ID">Indentification decryption of Address object (row)</param>
        /// <param name="locality">New city/town/village name</param>
        /// <param name="street">New street/square name</param>
        /// <param name="building">New building number</param>
        /// <returns>Information about the success of the operation</returns>
        public static String EditAddress(int ID, string locality, string street, string building)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Address_.Any(el => el.a_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        Address_ address = db.Address_.FirstOrDefault(el => el.a_id == ID);
                        address.a_locality = locality;
                        address.a_street = street;
                        address.a_building = building;
                        db.SaveChanges();
                        return "Item has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Receiving of all Address objects (rows)
        /// </summary>
        /// <returns>Collection of Address objects (rows)</returns>
        public static ObservableCollection<Address_> GetAllAddress()
        {
            var db = new TourfirmEntities(); 
            return new ObservableCollection<Address_>(db.Address_.ToList());
        }

        // Client
        public static String CreateClient(string surname, string name, string patronymic, int address, string telNum)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking the repetitions
                    bool isExist = db.Client.Any(el => el.cl_surname == surname && el.cl_telNum == telNum);

                    if (!isExist)
                    {
                        // Creating a new Client
                        Client newClient = new Client { cl_surname = surname, cl_name = name, cl_patronymic = patronymic, a_id = address, cl_telNum = telNum };
                        db.Client.Add(newClient);
                        db.SaveChanges();
                        return "New client has been added!";
                    }
                    else
                    {   // if there is a repetition
                        return "This client has already been added!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteClient(int ID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Client.Any(el => el.cl_id == ID);

                    if (isExist)
                    {
                        // Deleting
                        db.Client.Remove(db.Client.SingleOrDefault(x => x.cl_id == ID));
                        db.SaveChanges();
                        return "Client has been deleted!";
                    }
                    else
                    {   // if there no such item
                        return "There is no item with this id!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String EditClient(int ID, string surname, string name, string patronymic, int address, string telNum)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Client.Any(el => el.cl_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        Client client = db.Client.FirstOrDefault(el => el.cl_id == ID);
                        client.cl_surname = surname;
                        client.cl_name = name;
                        client.cl_patronymic = patronymic;
                        client.a_id = address;
                        client.cl_telNum = telNum;
                        db.SaveChanges();
                        return "Item has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static ObservableCollection<Client> GetAllClient()
        {
            var db = new TourfirmEntities();
            return new ObservableCollection<Client>(db.Client.Include("Country").ToList());
        }
        public static ObservableCollection<Country> GetAllClientVISAs(int ClientID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // return all VISAs of picked Client by id
                    Client client = db.Client.FirstOrDefault(el => el.cl_id == ClientID);
                    if (client != null)
                    {
                        return new ObservableCollection<Country>(client.Country.ToList());
                    }
                    else { return null; }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return null;
            }
        }
        public static String AddVISAForClient(int ClientID, int CountryID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking items existance
                    bool isExist1 = db.Client.Any(el => el.cl_id == ClientID);
                    bool isExist2 = db.Country.Any(el => el.c_id == CountryID);

                    if (isExist1 && isExist2)
                    {
                        // Addition of Country (VISA) to Client
                        Client client = db.Client.FirstOrDefault(el => el.cl_id == ClientID);
                        Country country = db.Country.FirstOrDefault(el => el.c_id == CountryID);
                        client.Country.Add(country);
                        db.SaveChanges();
                        return "New coutry has been added to client!";
                    }
                    else
                    {   // if there is no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteVISAFromClient(int ClientID, int CountryID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking items existance
                    bool isExist1 = db.Client.Any(el => el.cl_id == ClientID);
                    bool isExist2 = db.Country.Any(el => el.c_id == CountryID);

                    if (isExist1 && isExist2)
                    {
                        // Addition of Country (VISA) to Client
                        Client client = db.Client.FirstOrDefault(el => el.cl_id == ClientID);
                        Country country = db.Country.FirstOrDefault(el => el.c_id == CountryID);
                        client.Country.Remove(country);
                        db.SaveChanges();
                        return "Picked coutry (VISA) has been removed from client!";
                    }
                    else
                    {   // if there is no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static ObservableCollection<Client> GetAllClientsWithFilter(string filterText) 
        {
            try
            {
                if (filterText == null || filterText == "")
                {
                    return GetAllClient();
                }
                else
                {
                    using (var db = new TourfirmEntities())
                    {
                        List<Client> finalResult = new List<Client>();
                        
                        var results = db.Client.Where(p => p.cl_surname.Contains(filterText));
                        foreach (Client client in results)
                        {
                            finalResult.Add(client);
                        }
                        results = db.Client.Where(p => p.cl_name.Contains(filterText));
                        foreach (Client client in results)
                        {
                            finalResult.Add(client);
                        }
                        results = db.Client.Where(p => p.cl_patronymic.Contains(filterText));
                        foreach (Client client in results)
                        {
                            finalResult.Add(client);
                        }
                        results = db.Client.Where(p => p.cl_telNum.Contains(filterText));
                        foreach (Client client in results)
                        {
                            finalResult.Add(client);
                        }

                        try
                        {
                            var filterInt = Convert.ToInt32(filterText);
                            results = db.Client.Where(p => p.cl_id == filterInt);
                            foreach (Client client in results)
                            {
                                finalResult.Add(client);
                            }
                            results = db.Client.Where(p => p.a_id == filterInt);
                            foreach (Client client in results)
                            {
                                finalResult.Add(client);
                            }
                        }
                        catch
                        {
                            finalResult = finalResult.Distinct().ToList();
                            return new ObservableCollection<Client>(finalResult);
                        }

                        finalResult = finalResult.Distinct().ToList();
                        return new ObservableCollection<Client>(finalResult);
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        // Country
        public static String СreateCountry(string name, decimal visaPrice)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking the repetitions
                    bool isExist = db.Country.Any(el => el.c_name == name);

                    if (!isExist)
                    {
                        // Creating a new Country
                        Country newCountry = new Country { c_name = name, c_visaPrice = visaPrice};
                        db.Country.Add(newCountry);
                        db.SaveChanges();
                        return "New country has been added!";
                    }
                    else
                    {   // if there is a repetition
                        return "This country has already been added!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteCountry(int ID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Country.Any(el => el.c_id == ID);

                    if (isExist)
                    {
                        // Deleting
                        db.Country.Remove(db.Country.SingleOrDefault(x => x.c_id == ID));
                        db.SaveChanges();
                        return "Country has been deleted!";
                    }
                    else
                    {   // if there no such item
                        return "There is no item with this id!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String EditCountry(int ID, string name, decimal visaPrice)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Country.Any(el => el.c_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        Country country = db.Country.FirstOrDefault(el => el.c_id == ID);
                        country.c_name = name;
                        country.c_visaPrice = visaPrice;
                        db.SaveChanges();
                        return "Item has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static ObservableCollection<Country> GetAllCountry()
        {
            var db = new TourfirmEntities();
            return new ObservableCollection<Country>(db.Country.ToList());
        }

        // Employee
        public static String CreateEmployee(string surname, string name, string patronymic, int address, string telNum, byte[] img, int empPosititon)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking the repetitions
                    bool isExist = db.Employee.Any(el => el.e_surname == surname && el.e_telNum == telNum);

                    if (!isExist)
                    {
                        // Creating a new Employee
                        Employee newEmployee = new Employee { e_surname = surname, e_name = name, e_patronymic = patronymic, a_id = address, e_telNum = telNum, e_image = img, ep_id = empPosititon};
                        db.Employee.Add(newEmployee);
                        db.SaveChanges();
                        return "New employee has been added!";
                    }
                    else
                    {   // if there is a repetition
                        return "This employee has already been added!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteEmployee(int ID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Employee.Any(el => el.e_id == ID);

                    if (isExist)
                    {
                        // Deleting
                        db.Employee.Remove(db.Employee.SingleOrDefault(x => x.e_id == ID));
                        db.SaveChanges();
                        return "Employee has been deleted!";
                    }
                    else
                    {   // if there no such item
                        return "There is no item with this id!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String EditEmployee(int ID, string surname, string name, string patronymic, int address, string telNum, byte[] img, int empPosititon)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Employee.Any(el => el.e_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        Employee employee = db.Employee.FirstOrDefault(el => el.e_id == ID);
                        employee.e_surname = surname;
                        employee.e_name = surname;
                        employee.e_patronymic = surname;
                        employee.a_id = address;
                        employee.e_telNum = telNum;
                        employee.e_image = img;
                        employee.ep_id = empPosititon;
                        db.SaveChanges();
                        return "Item has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }

        public static String EditEmployeeImage(int ID, byte[] img)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Employee.Any(el => el.e_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        Employee employee = db.Employee.FirstOrDefault(el => el.e_id == ID);
                        employee.e_image = img;
                        db.SaveChanges();
                        return "Image has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static ObservableCollection<Employee> GetAllEmployee()
        {
            var db = new TourfirmEntities();
            return new ObservableCollection<Employee>(db.Employee.ToList());
        }

        // Employee position
        public static String CreateEmployeePosition(string name, decimal salary)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking the repetitions
                    bool isExist = db.Employee_position.Any(el => el.ep_name == name);

                    if (!isExist)
                    {
                        // Creating a new Employee position
                        Employee_position newEmployeePosition = new Employee_position { ep_name = name, ep_salary = salary};
                        db.Employee_position.Add(newEmployeePosition);
                        db.SaveChanges();
                        return "New employee has been added!";
                    }
                    else
                    {   // if there is a repetition
                        return "This employee has already been added!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteEmployeePosition(int ID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Employee_position.Any(el => el.ep_id == ID);

                    if (isExist)
                    {
                        // Deleting
                        db.Employee_position.Remove(db.Employee_position.SingleOrDefault(x => x.ep_id == ID));
                        db.SaveChanges();
                        return "Employee position has been deleted!";
                    }
                    else
                    {   // if there no such item
                        return "There is no item with this id!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String EditEmployeePosition(int ID, string name, decimal salary)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Employee_position.Any(el => el.ep_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        Employee_position empPosition = db.Employee_position.FirstOrDefault(el => el.ep_id == ID);
                        empPosition.ep_name = name;
                        empPosition.ep_salary = salary;
                        db.SaveChanges();
                        return "Item has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static ObservableCollection<Employee_position> GetAllEmployeePosition()
        {
            var db = new TourfirmEntities();
            return new ObservableCollection<Employee_position>(db.Employee_position.ToList());
        }

        // Route 
        public static String CreateRoute(int departureCountry, int arrivalCountry)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking the repetitions
                    bool isExist = db.Route_.Any(el => el.dc_id == departureCountry && el.ac_id == arrivalCountry);

                    if (!isExist)
                    {
                        // Creating a new Route
                        Route_ newRoute = new Route_ {ac_id = arrivalCountry, dc_id = departureCountry};
                        db.Route_.Add(newRoute);
                        db.SaveChanges();
                        return "New route has been added!";
                    }
                    else
                    {   // if there is a repetition
                        return "This route has already been added!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteRoute(int ID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Route_.Any(el => el.r_id == ID);

                    if (isExist)
                    {
                        // Deleting
                        db.Route_.Remove(db.Route_.SingleOrDefault(x => x.r_id == ID));
                        db.SaveChanges();
                        return "Route has been deleted!";
                    }
                    else
                    {   // if there no such item
                        return "There is no item with this id!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String EditRoute(int ID, int departureCountry, int arrivalCountry)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Route_.Any(el => el.r_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        Route_ route = db.Route_.FirstOrDefault(el => el.r_id == ID);
                        route.dc_id = departureCountry;
                        route.ac_id = arrivalCountry;
                        db.SaveChanges();
                        return "Item has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static ObservableCollection<Route_> GetAllRoute()
        {
            var db = new TourfirmEntities();
            return new ObservableCollection<Route_>(db.Route_.ToList());
        }

        // Sale
        public static String CreateSale(int employee, int client, System.DateTime saleDate)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Creating a new Sale
                    Sale newSale = new Sale { e_id = employee, cl_id = client, s_tDate = saleDate };
                    db.Sale.Add(newSale);
                    db.SaveChanges();
                    return "New sale has been added!";

                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteSale(int ID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Sale.Any(el => el.s_id == ID);

                    if (isExist)
                    {
                        // Deleting
                        db.Sale.Remove(db.Sale.SingleOrDefault(x => x.s_id == ID));
                        db.SaveChanges();
                        return "Sale has been deleted!";
                    }
                    else
                    {   // if there no such item
                        return "There is no item with this id!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String EditSale(int ID, int employee, int client, System.DateTime saleDate)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Sale.Any(el => el.s_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        Sale sale = db.Sale.FirstOrDefault(el => el.s_id == ID);
                        sale.e_id = employee;
                        sale.cl_id = client;
                        sale.s_tDate = saleDate;
                        db.SaveChanges();
                        return "Item has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static ObservableCollection<Sale> GetAllSale()
        {
            var db = new TourfirmEntities();
            return new ObservableCollection<Sale>(db.Sale.Include("Trip").ToList());
        }
        public static ObservableCollection<Trip> GetAllSaleTrips(int SaleID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // return all trips of picked sale by id
                    Sale sale = db.Sale.FirstOrDefault(el => el.s_id == SaleID);
                    if (sale != null)
                    {
                        return new ObservableCollection<Trip>(sale.Trip.ToList());
                    }
                    else { return null; }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return null;
            }
        }
        public static String AddTripForSale(int SaleID, int TripID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking items existance
                    bool isExist1 = db.Sale.Any(el => el.s_id == SaleID);
                    bool isExist2 = db.Trip.Any(el => el.t_id == TripID);

                    if (isExist1 && isExist2)
                    {
                        // Addition of Trip to Sale
                        Sale sale = db.Sale.FirstOrDefault(el => el.s_id == SaleID);
                        Trip trip = db.Trip.FirstOrDefault(el => el.t_id == TripID);
                        sale.Trip.Add(trip);
                        db.SaveChanges();
                        return "New trip has been added to sale!";
                    }
                    else
                    {   // if there is no such items
                        return "There is no such item(s)!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteTripFromSale(int SaleID, int TripID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking items existance
                    bool isExist1 = db.Sale.Any(el => el.s_id == SaleID);
                    bool isExist2 = db.Trip.Any(el => el.t_id == TripID);

                    if (isExist1 && isExist2)
                    {
                        // Addition of Trip to Sale
                        Sale sale = db.Sale.FirstOrDefault(el => el.s_id == SaleID);
                        Trip trip = db.Trip.FirstOrDefault(el => el.t_id == TripID);
                        sale.Trip.Remove(trip);
                        db.SaveChanges();
                        return "Picked trip has been removed from sale!";
                    }
                    else
                    {   // if there is no such items
                        return "There is no such item(s)!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }

        // Trip
        public static String CreateTrip(decimal price, System.DateTime tripDate, int route)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking the repetitions
                    bool isExist = db.Trip.Any(el => el.t_price == price && el.t_date == tripDate && el.r_id == route);

                    if (!isExist)
                    {
                        // Creating a new Employee
                        Trip newTrip = new Trip { t_price = price, t_date = tripDate, r_id = route };
                        db.Trip.Add(newTrip);
                        db.SaveChanges();
                        return "New trip has been added!";
                    }
                    else
                    {   // if there is a repetition
                        return "This trip has already been added!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteTrip(int ID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Trip.Any(el => el.t_id == ID);

                    if (isExist)
                    {
                        // Deleting
                        db.Trip.Remove(db.Trip.SingleOrDefault(x => x.t_id == ID));
                        db.SaveChanges();
                        return "Trip has been deleted!";
                    }
                    else
                    {   // if there no such item
                        return "There is no item with this id!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String EditTrip(int ID, decimal price, System.DateTime tripDate, int route)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.Trip.Any(el => el.t_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        Trip trip = db.Trip.FirstOrDefault(el => el.t_id == ID);
                        trip.t_price = price;
                        trip.t_date = tripDate;
                        trip.r_id = route;
                        db.SaveChanges();
                        return "Item has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static ObservableCollection<Trip> GetAllTrip()
        {
            var db = new TourfirmEntities();
            return new ObservableCollection<Trip>(db.Trip.Include("Goal").ToList());
        }
        public static ObservableCollection<Goal> GetAllTripGoals(int TripID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // return all goals of picked trip by trip id
                    Trip trip = db.Trip.FirstOrDefault(el => el.t_id == TripID);
                    if (trip != null)
                    {
                        return new ObservableCollection<Goal>(trip.Goal.ToList());
                    }
                    else { return null; }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return null;
            }
        }
        public static String AddGoalToTrip(int TripID, int GoalID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking items existance
                    bool isExist1 = db.Trip.Any(el => el.t_id == TripID);
                    bool isExist2 = db.Goal.Any(el => el.g_id == GoalID);

                    if (isExist1 && isExist2)
                    {
                        // Addition of Trip to Sale
                        Trip trip = db.Trip.FirstOrDefault(el => el.t_id == TripID);
                        Goal goal = db.Goal.FirstOrDefault(el => el.g_id == GoalID);
                        trip.Goal.Add(goal);
                        db.SaveChanges();
                        return "New goal has been added to trip!";
                    }
                    else
                    {   // if there is no such item
                        return "There is no such item(s)!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteGoalFromTrip(int TripID, int GoalID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking items existance
                    bool isExist1 = db.Trip.Any(el => el.t_id == TripID);
                    bool isExist2 = db.Goal.Any(el => el.g_id == GoalID);

                    if (isExist1 && isExist2)
                    {
                        // Addition of Trip to Sale
                        Trip trip = db.Trip.FirstOrDefault(el => el.t_id == TripID);
                        Goal goal = db.Goal.FirstOrDefault(el => el.g_id == GoalID);
                        trip.Goal.Remove(goal);
                        db.SaveChanges();
                        return "Picked goal has been removed from trip!";
                    }
                    else
                    {   // if there is no such item
                        return "There is no such item(s)!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }

        // User
        public static String CreateUser(string login, string password, byte userType)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking the repetitions
                    bool isExist = db.User_.Any(el => el.u_login == login);

                    if (!isExist)
                    {
                        // Creating a new User
                        User_ newUser = new User_ { u_login = login, u_password = password, ut_id = userType, u_isActive = false};
                        db.User_.Add(newUser);
                        db.SaveChanges();
                        return "New user has been added!";
                    }
                    else
                    {   // if there is a repetition
                        return "This user has already been added!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteUser(int ID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.User_.Any(el => el.u_id == ID);

                    if (isExist)
                    {
                        // Deleting
                        db.User_.Remove(db.User_.SingleOrDefault(x => x.u_id == ID));
                        db.SaveChanges();
                        return "User has been deleted!";
                    }
                    else
                    {   // if there no such item
                        return "There is no item with this id!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String EditUser(int ID, string login, string password, byte userType)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.User_.Any(el => el.u_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        User_ user = db.User_.FirstOrDefault(el => el.u_id == ID);
                        user.u_login = login;
                        user.u_password = password;
                        user.ut_id = userType;
                        db.SaveChanges();
                        return "Item has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static ObservableCollection<User_> GetAllUser()
        {
            var db = new TourfirmEntities();
            return new ObservableCollection<User_>(db.User_.ToList());
        }

        public static User_ GetUserByLoginAndPassword(string login, string password)
        {
            if (login != String.Empty && password != String.Empty)
            {
                login = login.Trim(' ');
                password = password.Trim(' ');
                try
                {
                    using (TourfirmEntities db = new TourfirmEntities())
                    {
                        User_ user = db.User_.FirstOrDefault(el => el.u_login == login && el.u_password == password);
                        return user;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        

        // User type
        public static String CreateUserType(string name)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking the repetitions
                    bool isExist = db.User_type.Any(el => el.ut_name == name);

                    if (!isExist)
                    {
                        // Creating a new user type
                        User_type newUserType = new User_type { ut_name = name };
                        db.User_type.Add(newUserType);
                        db.SaveChanges();
                        return "New user type has been added!";
                    }
                    else
                    {   // if there is a repetition
                        return "This user type has already been added!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String DeleteUserType(int ID)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.User_type.Any(el => el.ut_id == ID);

                    if (isExist)
                    {
                        // Deleting
                        db.User_type.Remove(db.User_type.SingleOrDefault(x => x.ut_id == ID));
                        db.SaveChanges();
                        return "User type has been deleted!";
                    }
                    else
                    {   // if there no such item
                        return "There is no item with this id!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static String EditUserType(int ID, string name)
        {
            try
            {
                using (TourfirmEntities db = new TourfirmEntities())
                {
                    // Checking is there such item
                    bool isExist = db.User_type.Any(el => el.ut_id == ID);

                    if (isExist)
                    {
                        // Data editing
                        User_type userType = db.User_type.FirstOrDefault(el => el.ut_id == ID);
                        userType.ut_name = name;
                        db.SaveChanges();
                        return "Item has been edited!";
                    }
                    else
                    {   // if there no such item
                        return "There is no such item!";
                    }
                }
            }
            catch (Exception ex) //unexpected errors and etc.
            {
                return ex.Message;
            }
        }
        public static ObservableCollection<User_type> GetAllUserType()
        {
            var db = new TourfirmEntities();
            return new ObservableCollection<User_type>(db.User_type.ToList());
        }
    }
}
