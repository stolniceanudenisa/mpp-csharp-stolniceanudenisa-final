using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientCsharp.database;
using ClientCsharp.service;
using CSharpInterface;
using CSharpInterface.gui;
using log4net;
using log4net.Config;
using mpp_csharp_stolniceanudenisa_final.model;

namespace ClientCsharp
{
    internal class Program
    {
 
        public static void Main(string[] args)
        {
  

            Application.SetCompatibleTextRenderingDefault(false);
            
            IDictionary<String, string> props2 = new Dictionary<string, string>();
             props2.Add("ConnectionString", GetConnectionStringByName("mppDB"));
            
            
             UserDbRepository userDbRepository = new UserDbRepository(props2);
             FlightDbRepository flightDbRepository = new FlightDbRepository(props2);
             BookingDbRepository  bookingDbRepository= new BookingDbRepository(props2);
             ClientDbRepository clientDbRepository = new ClientDbRepository(props2);


            Service srv = new Service(userDbRepository, bookingDbRepository, clientDbRepository, flightDbRepository);
            
            login login = new login();
            login.service = srv;
             Application.EnableVisualStyles();
             Application.Run(login);
      
             
            // pt celelalte forms
            // Form1 main = new Form1();
            // main.service = srv;
            // Application.EnableVisualStyles();
            // Application.Run(main);
        }
        
        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

    }

}


//
// namespace ClientCsharp
// {
//     static class Program
//     {
//         /// <summary>
//         /// The main entry point for the application.
//         /// </summary>
//         [STAThread]
//         static void Main()
//         {
//             Application.EnableVisualStyles();
//             Application.SetCompatibleTextRenderingDefault(false);
//             Application.Run(new Form1());
//             
//             IDictionary<String, string> props2 = new Dictionary<string, string>();
//             props2.Add("ConnectionString", GetConnectionStringByName("mppDB"));
//             
//             
//             ILog logger = LogManager.GetLogger(typeof(Program));    
//
//             UserDbRepository userDbRepository = new UserDbRepository(props2);
//             FlightDbRepository flightDbRepository = new FlightDbRepository(props2);
//             BookingDbRepository bookingDbRepository = new BookingDbRepository(props2);
//             ClientDbRepository clientDbRepository = new ClientDbRepository(props2);
//
//             //Service service = new Service(userDbRepository, flightDbRepository, clientDbRepository, clientDbRepository);
//
//      
//             
//             // ApplicationConfiguration.Initialize();
//             // login Login = new login();
//             // Login.setService(service);
//             // logger.Info("Application strated");
//             // Application.Run(Login);
//
//             
//             
//             static string GetConnectionStringByName(string name)
//             {
//                 // Assume failure.
//                 string returnValue = null;
//
//                 // Look for the name in the connectionStrings section.
//                 ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];
//
//                 // If found, return the connection string.
//                 if (settings != null)
//                     returnValue = settings.ConnectionString;
//
//                 return returnValue;
//             }
//         }
//     }
// }