using System;
using System.Collections.Generic;
using mpp_csharp_stolniceanudenisa_final.model;

namespace mpp_csharp_stolniceanudenisa_final.persistence
{
    public interface IFlightRepository: IRepository<long, Flight>
    {
        IEnumerable<Flight> FindFlightByDestinationAndDate(string destination, DateTime dateTime);
    }
}