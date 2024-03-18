using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using mpp_csharp_stolniceanudenisa_final.model;
using mpp_csharp_stolniceanudenisa_final.persistence;

namespace mpp_csharp_stolniceanudenisa_final.database;

public class FlightDbRepository: IFlightRepository
{
    private static readonly ILog log = LogManager.GetLogger("FlightDbRepository");
    private IDictionary<String, string> props;
    
    public FlightDbRepository(IDictionary<String, string> props)
    {
        log.Info("Creating FlightDbRepository ");
        this.props = props;
    }
    
    public Flight findOne(long id)
    {
        log.InfoFormat("Entering FlightDbRepository findOne with value {0}", id);
        IDbConnection con = DBUtils.getConnection(props);
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM flights WHERE flight_id = @id";
            IDbDataParameter paramId = comm.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            comm.Parameters.Add(paramId);
            using (var dataR = comm.ExecuteReader())
            {
                if (dataR.Read())
                {
                    long flightId = dataR.GetInt32(0);
                    var destination = dataR.GetString(1);
                    
                    long unixTimestamp = dataR.GetInt64(2);
                    var departureDateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).UtcDateTime;
                
                    var airport = dataR.GetString(3);
                    var availableSeats = dataR.GetInt32(4);
    
                    Flight flight = new Flight(destination, departureDateTime, airport, availableSeats);
                    flight.id = flightId;
                    log.InfoFormat("Exiting FlightDbRepository findOne with value {0}", flight);
                    return flight;
                }
            }
        }
        log.InfoFormat("Exiting FlightDbRepository findOne with value {0}", null);
        return null;
    }
    
    
  
    
    public IEnumerable<Flight> GetAll()
    {
        log.Info("Entering FlightDbRepository GetAll Flights");
        IDbConnection con = DBUtils.getConnection(props);
        IList<Flight> flights = new List<Flight>();
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM flights";
        
            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    long id = dataR.GetInt32(0);
                    var destination = dataR.GetString(1);
                
                    // Parse the Unix timestamp to a DateTime object
                    long unixTimestamp = dataR.GetInt64(2);
                    var departureDateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).UtcDateTime;
                
                    var airport = dataR.GetString(3);
                    var availableSeats = dataR.GetInt32(4);

                    Flight flight = new Flight(destination, departureDateTime, airport, availableSeats);
                    flight.id = id;
                    flights.Add(flight);
                }
            }
        }
        log.Info("Exiting FlightDbRepository GetAll Flights");
        return flights;
    }

 
    public void Add(Flight entity)
    {
    log.InfoFormat("Entering FlightDbRepository Add value {0}", entity);
    using (IDbConnection connection = DBUtils.getConnection(props))
    {
        log.InfoFormat("FlightDbRepository Add opened connection to database {0}", connection);
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO flights(destination, departure_date_time, airport, available_seats) VALUES (@destination, @departureDateTime, @airport, @availableSeats)";

            IDbDataParameter paramDestination = command.CreateParameter();
            paramDestination.ParameterName = "@destination";
            paramDestination.Value = entity.Destination;
            command.Parameters.Add(paramDestination);

            // Convert departureDateTime to Unix timestamp
            long unixTimestamp = (long)(entity.DepartureDateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            IDbDataParameter paramDepartureDateTime = command.CreateParameter();
            paramDepartureDateTime.ParameterName = "@departureDateTime";
            paramDepartureDateTime.Value = unixTimestamp;
            command.Parameters.Add(paramDepartureDateTime);

            IDbDataParameter paramAirport = command.CreateParameter();
            paramAirport.ParameterName = "@airport";
            paramAirport.Value = entity.Airport;
            command.Parameters.Add(paramAirport);

            IDbDataParameter paramAvailableSeats = command.CreateParameter();
            paramAvailableSeats.ParameterName = "@availableSeats";
            paramAvailableSeats.Value = entity.AvailableSeats;
            command.Parameters.Add(paramAvailableSeats);

            var result = command.ExecuteNonQuery();
            if (result == 0)
            {
                log.Error("FlightDbRepository Add failed!");
                throw new Exception("No flight added!");
            }
        }
    }
    log.InfoFormat("Exiting FlightDbRepository Add with value {0}", entity);
    }


    public void Clear()
    {
        throw new NotImplementedException();
    }

    public void Update(Flight entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(long id)
    {
        log.InfoFormat("Entering FlightDbRepository Delete with value {0}", id);
        IDbConnection con = DBUtils.getConnection(props);
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "DELETE FROM flights WHERE flight_id = @id";
            IDbDataParameter paramId = comm.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            comm.Parameters.Add(paramId);
            var result = comm.ExecuteNonQuery();
            if (result == 0)
            {
                log.Error("FlightDbRepository Delete failed!");
                throw new Exception("No user deleted !");
            }
        }
        log.InfoFormat("Exiting FlightDbRepository Delete with value {0}", id);
    }
    
 
    public IEnumerable<Flight> FindFlightByDestinationAndDate(string destination, DateTime dateTime)
    {
        
        log.InfoFormat("Entering FlightDbRepository FindFlightByDestinationAndDate with value {0}", destination);
        IDbConnection con = DBUtils.getConnection(props);
        IList<Flight> flights = new List<Flight>();
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM flights WHERE destination = @destination AND departure_date_time = @departureDateTime";
            IDbDataParameter paramDestination = comm.CreateParameter();
            paramDestination.ParameterName = "@destination";
            paramDestination.Value = destination;
            comm.Parameters.Add(paramDestination);
            
            // Convert departureDateTime to Unix timestamp
            long unixTimestamp = (long)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            IDbDataParameter paramDepartureDateTime = comm.CreateParameter();
            paramDepartureDateTime.ParameterName = "@departureDateTime";
            paramDepartureDateTime.Value = unixTimestamp;
            comm.Parameters.Add(paramDepartureDateTime);
            
            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    long id = dataR.GetInt32(0);
                    var destinationFlight = dataR.GetString(1);
                
                    // Parse the Unix timestamp to a DateTime object
                    long unixTimestampFlight = dataR.GetInt64(2);
                    var departureDateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestampFlight).UtcDateTime;
                
                    var airport = dataR.GetString(3);
                    var availableSeats = dataR.GetInt32(4);

                    Flight flight = new Flight(destinationFlight, departureDateTime, airport, availableSeats);
                    flight.id = id;
                    flights.Add(flight);

                    log.InfoFormat("Exiting FlightDbRepository FindFlightByDestinationAndDate  with value {0}", flight);
                }
            }
        }
        log.Info("Exiting FlightDbRepository FindFlightByDestinationAndDate");
        return flights;
    }
}