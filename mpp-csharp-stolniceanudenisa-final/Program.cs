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
            UserDbRepository repo = new UserDbRepository(props2);
            // repo.Add(new User(999,"ioana12_","091232"));
            // repo.FindUserByCredentials("mihai_ilie","123"); //merge
            foreach (User user in repo.GetAll())
            {
                Console.WriteLine(user);
            }
            
            
            
            
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