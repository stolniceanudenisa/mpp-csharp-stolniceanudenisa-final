using System;

namespace mpp_csharp_stolniceanudenisa_final
{
    internal class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public static void Main(string[] args)
        {

            log.Info("Aprilie!");
            Console.WriteLine("E bine");
            
            // System.Console.WriteLine("Hello, World!");
        }
    }
}