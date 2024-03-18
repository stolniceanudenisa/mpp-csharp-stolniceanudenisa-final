using System;
using System.Collections.Generic;
using System.Configuration;
using log4net.Config;
using mpp_csharp_stolniceanudenisa_final.database;
using mpp_csharp_stolniceanudenisa_final.model;
using mpp_csharp_stolniceanudenisa_final.persistence;

namespace mpp_csharp_stolniceanudenisa_final
{
    internal class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public static void Main(string[] args)
        {

            log.Info("Iunie!");
            Console.WriteLine("E bine");
            
  
            //metoda 1
            // var props = new Dictionary<string, string>();
            // props["ConnectionString"] = "Data Source=C:\\Users\\40766\\IdeaProjects\\mpp-proiect-java-stolniceanudenisa\\mpp_db.sqlite;Version=3;"; 
            // IUserRepository userRepository = new UserDbRepository(props);
            //
            // IEnumerable<User> users = userRepository.GetAll();
            //
            // foreach (var user in users)
            // {
            //     Console.WriteLine(user);
            // }
            //
            
           
            //configurare jurnalizare folosind log4net in fisier xml
            // XmlConfigurator.Configure(new System.IO.FileInfo("apptest.config"));
            //
            // log.Info("Hello logging world!");
            // Console.WriteLine("ABC");
            //
            
            
            Console.WriteLine("Configuration Settings for mppDB {0}",GetConnectionStringByName("mppDB"));
            
            IDictionary<String, string> props2 = new Dictionary<string, string>();
            props2.Add("ConnectionString", GetConnectionStringByName("mppDB"));
            
            Console.WriteLine("Users");
            UserDbRepository repoUser = new UserDbRepository(props2);
            // repo.Add(new User(1001,"ana12_","091232"));         // dupa add mi se inchide conexiunea de remediat !!!!!!!!!!!!!!!
            // repo.Add(new User("mihaela","091232")); 
            // repoUser.findOne(2);
            // repo.FindUserByCredentials("mihai_ilie","123");
            // repo.Delete(1000);
            
            
            Console.WriteLine();Console.WriteLine();
            Console.WriteLine("Users");
            foreach (User user in repoUser.GetAll())
            {
                Console.WriteLine(user);
            }
            Console.WriteLine();Console.WriteLine();Console.WriteLine();
            
            
            FlightDbRepository repoFlight = new FlightDbRepository(props2);
            // repoFlight.Add(new Flight("Craiova", new DateTime(2021, 6, 15, 12, 0, 0), "Chisinau", 100));
            // repoFlight.Delete(9);
            // repoFlight.findOne(2);
            //repoFlight.FindFlightByDestinationAndDate("Craiova", new DateTime(2021, 6, 15, 12, 0, 0)); 
            
            Console.WriteLine();Console.WriteLine();
            Console.WriteLine("Flights");
            foreach (Flight flight in repoFlight.GetAll())
            {
                Console.WriteLine(flight);
            }
            Console.WriteLine();Console.WriteLine();Console.WriteLine();
            
            
            BookingDbRepository repoBooking = new BookingDbRepository(props2);
            //repoBooking.findOne(1);
            
            // Client client = new Client("AAAAAAA", "BBBBBBBB"); // Adjust client details as needed
            // client.id = 3;
            
            // Flight flight = new Flight("CCCCCCC", DateTime.Now, "DDDDDDDDD", 100); // Adjust flight details as needed
            // flight.id = 5;
            
            // List<string> passengers = new List<string> { "WWW", "EEEE", "SSSS" }; // Add passengers as needed
            
            // Booking newBooking = new Booking(flight, client, passengers);
            
            // repoBooking.Add(newBooking);
            
            // repoBooking.Delete(15);
            
            //int numberOfBookings = repoBooking.GetNumberOfBookingsForFlight(repoFlight.findOne(8L));   //nu merge bine de rezolvat conexiunea la baze de date
            //Console.WriteLine($"Number of bookings for flight {repoFlight.findOne(8L).Destination}: {numberOfBookings}");
            
            Console.WriteLine();Console.WriteLine();
            Console.WriteLine("Bookings");
            foreach (Booking booking in repoBooking.GetAll())
            {
                Console.WriteLine(booking);
            }
            Console.WriteLine();Console.WriteLine();Console.WriteLine();
            
            
  
            ClientDbRepository repoClient = new ClientDbRepository(props2);
            //repoClient.findOne(1);
            //repoClient.Add(new Client("Simina", "Maria"));
            // repoClient.Delete(7);
            
            
            Console.WriteLine();Console.WriteLine();
            Console.WriteLine("Clients");
            Console.WriteLine();Console.WriteLine();Console.WriteLine();
            foreach (Client client in repoClient.GetAll())
            {
                Console.WriteLine(client);
            }
            Console.WriteLine();Console.WriteLine();Console.WriteLine();
            
            
            
            static string GetConnectionStringByName(string name)
            {
                // Assume failure.
                string returnValue = null;

                // Look for the name in the connectionStrings section.
                ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];

                // If found, return the connection string.
                if (settings != null)
                    returnValue = settings.ConnectionString;

                return returnValue;
            }
        }
    }
}