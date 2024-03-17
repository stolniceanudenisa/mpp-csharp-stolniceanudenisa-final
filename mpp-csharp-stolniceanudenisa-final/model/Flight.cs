using System;

namespace mpp_csharp_stolniceanudenisa_final.model
{
    public class Flight: Entity<long>
    {
        public string Destination { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string Airport { get; set; }
        public int AvailableSeats { get; set; }

        public Flight(long id, string destination, DateTime departureDateTime, string airport, int availableSeats):base(id)
        {
            Destination = destination;
            DepartureDateTime = departureDateTime;
            Airport = airport;
            AvailableSeats = availableSeats;
        }
        
        public override string ToString()
     {
         return "Flight: " +
                "destination: '" + Destination + '\'' +
                ", departureDateTime: " + DepartureDateTime +
                ", airport: " + Airport + '\'' +
                ", availableSeats: " + AvailableSeats + '\'';
     }
    }
}