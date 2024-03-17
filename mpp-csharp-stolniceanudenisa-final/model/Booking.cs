using System.Collections.Generic;

namespace mpp_csharp_stolniceanudenisa_final.model
{
    public class Booking: Entity<long>
    {
        public Flight Flight { get; set; }
         public Client Client { get; set; }
         public List<string> Passengers { get; set; }

         public Booking(long id, Flight flight, Client client, List<string> passengers):base(id)
         {
             Flight = flight;
             Client = client;
             this.Passengers = passengers;
         }
         
         public override string ToString()
         {
             return "Booking: " +
                    "flight: " + Flight +
                    ", client: " + Client +
                    ", passengers: " + string.Join(", ", Passengers) + '\'';
         }
    }
}