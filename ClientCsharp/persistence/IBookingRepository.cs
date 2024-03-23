using mpp_csharp_stolniceanudenisa_final.model;
 

namespace ClientCsharp.persistence
{
    public interface IBookingRepository : IRepository<long, Booking>
    {
        int GetNumberOfBookingsForFlight(Flight flight);
        int GtNumberOfBookingsForFlightById(long flightId);
        long GetLowestAvailableId();
        
        
    }
}