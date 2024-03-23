using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using log4net;
using mpp_csharp_stolniceanudenisa_final.model;
using ClientCsharp.persistence;

namespace ClientCsharp.database;

public class BookingDbRepository: IBookingRepository
{
    
    private static readonly ILog log = LogManager.GetLogger("BookingDbRepository");
    private IDictionary<String, string> props;
    
    
    public BookingDbRepository(IDictionary<String, string> props)
    {
        log.Info("Creating BookingDbRepository ");
        this.props = props;
    }
    
public Booking findOne(long id)
{
    log.InfoFormat("Entering BookingDbRepository findOne with id {0}", id);
    
    using (IDbConnection connection = DBUtils.getConnection(props))
    {
        log.InfoFormat("BookingDbRepository FindOne opened connection to database {0}", connection);
        
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM bookings b " +
                                  "INNER JOIN clients c ON c.client_id = b.client_id " +
                                  "INNER JOIN flights f ON f.flight_id = b.flight_id " +
                                  "WHERE b.booking_id = @id";

            IDbDataParameter paramId = command.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            command.Parameters.Add(paramId);

            using (IDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    long bookingId = dataReader.GetInt64(dataReader.GetOrdinal("booking_id"));
                    long clientId = dataReader.GetInt64(dataReader.GetOrdinal("client_id"));
                    string clientsName = dataReader.GetString(dataReader.GetOrdinal("clients_name"));
                    long flightId = dataReader.GetInt64(dataReader.GetOrdinal("flight_id"));
                    string destination = dataReader.GetString(dataReader.GetOrdinal("destination"));
                    
                    DateTime departureDateTime;

                    // Example: If departureDateTime is stored as Unix timestamp (milliseconds)
                    long unixTimestamp = dataReader.GetInt64(dataReader.GetOrdinal("departure_date_time"));
                    departureDateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).UtcDateTime;
                    
                    string airport = dataReader.GetString(dataReader.GetOrdinal("airport"));
                    int availableSeats = dataReader.GetInt32(dataReader.GetOrdinal("available_seats"));

                    Client client = new Client(clientsName, "");  
                    client.id = clientId;

                    Flight flight = new Flight(destination, departureDateTime, airport, availableSeats);
                    flight.id = flightId;

                    List<string> passengers = new List<string>();  

                    Booking booking = new Booking(flight, client, passengers);
                    log.InfoFormat("Exiting BookingDbRepository findOne with value {0}", booking);
                    return booking;
                }
                else
                {
                    return null; // No booking found with the given ID
                }
            }
        }
    }
}


    public IEnumerable<Booking> GetAll()
    {
        
        log.Info("Entering BookingDbRepository GetAll Bookings");
        IDbConnection con = DBUtils.getConnection(props);
        IList<Booking> bookings = new List<Booking>();
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM bookings b " +
                  "INNER JOIN clients c on c.client_id = b.client_id " +
                  "INNER JOIN flights f on f.flight_id = b.flight_id";

            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    long id = dataR.GetInt32(0);
                    var clientsString = dataR.GetString(3);
                    
                    long idClient = dataR.GetInt32(4);
                    var nameClient = dataR.GetString(5);
                    var addressClient = dataR.GetString(6);

                    Client client = new Client(nameClient, addressClient);
                    client.id = idClient;
                    
                    long idFlight = dataR.GetInt32(7);
                    var destination = dataR.GetString(8);
                    
                    DateTime departureDateTime;

                    // Example: If departureDateTime is stored as Unix timestamp (milliseconds)
                    long unixTimestamp = dataR.GetInt64(9);
                    departureDateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).UtcDateTime;

                    
                     
                    var airport = dataR.GetString(10);
                    var availableSeats = dataR.GetInt32(11);

                    Flight flight = new Flight(destination, departureDateTime, airport, availableSeats);
                    flight.id = idFlight;

                    var clients = clientsString.Split(',');

                    Booking booking = new Booking(flight, client, new List<string>(clients));
                    booking.id = id;
                    
                    bookings.Add(booking);
                }
            }
        }
        log.Info("Exiting BookingDbRepository GetAll Bookings");
        return bookings;
    }

    public void Add(Booking entity)
    {
       
        log.InfoFormat("Entering BookingDbRepository Add with value {0}", entity);
        using (IDbConnection connection = DBUtils.getConnection(props))
        {
            log.InfoFormat("BookingDbRepository Add opened connection to database {0}", connection);
            
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO bookings (client_id, flight_id, clients_name) VALUES (@clientId, @flightId, @passengers)";
                
                IDbDataParameter paramClientId = command.CreateParameter();
                paramClientId.ParameterName = "@clientId";
                paramClientId.Value = entity.Client.id;
                command.Parameters.Add(paramClientId);
                
                IDbDataParameter paramFlightId = command.CreateParameter();
                paramFlightId.ParameterName = "@flightId";
                paramFlightId.Value = entity.Flight.id;
                command.Parameters.Add(paramFlightId);
                
                IDbDataParameter paramPassengers = command.CreateParameter();
                paramPassengers.ParameterName = "@passengers";
                paramPassengers.Value = string.Join(",", entity.Passengers);
                command.Parameters.Add(paramPassengers);
                
                var result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    log.InfoFormat("Exiting BookingDbRepository Add with value {0}", entity);
                    throw new RepositoryException("No booking added !");
                }
            }
        }
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public void Update(Booking entity)
    {
        throw new System.NotImplementedException();
    }

    public void Delete(long id)
    {
        log.InfoFormat("Entering BookingDbRepository Delete with id {0}", id);
        using (IDbConnection connection = DBUtils.getConnection(props))
        {
            log.InfoFormat("BookingDbRepository Delete opened connection to database {0}", connection);
            
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM bookings WHERE booking_id = @id";
                
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                command.Parameters.Add(paramId);
                
                var result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    log.InfoFormat("Exiting BookingDbRepository Delete with id {0}", id);
                    throw new RepositoryException("No booking deleted !");
                }
            }
        }
    }

    public int GetNumberOfBookingsForFlight(Flight flight)
    {
       
        log.InfoFormat("Entering BookingDbRepository GetNumberOfBookingsForFlight with value {0}", flight);
        using (IDbConnection connection = DBUtils.getConnection(props))
        {
            log.InfoFormat("BookingDbRepository GetNumberOfBookingsForFlight opened connection to database {0}", connection);
            
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM bookings WHERE flight_id = @id";
                
                IDbDataParameter paramId = command.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = flight.id;
                command.Parameters.Add(paramId);
                
                using (IDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        int result = dataReader.GetInt32(0);
                        log.InfoFormat("Exiting BookingDbRepository GetNumberOfBookingsForFlight with value {0}", result);
                        return result;
                    }
                    else
                    {
                        log.InfoFormat("Exiting BookingDbRepository GetNumberOfBookingsForFlight with value {0}", 0);
                        return 0;
                    }
                }
            }
        }
    }

    public int GtNumberOfBookingsForFlightById(long flightId)
    {
        throw new System.NotImplementedException();
    }

    public long GetLowestAvailableId()
    {
        log.Info("Booking - getLowestAvailableId - Enter");
        long id = 0;
        IDbConnection con = DBUtils.getConnection(props);
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT MAX(booking_id) FROM bookings"; 
            using (var dataR = comm.ExecuteReader())
            {
                if (dataR.Read())
                {
                    id = dataR.GetInt64(0);
                }
            }
        }
        log.Info("Booking - getLowestAvailableId - Exit");
        return id;
    }
}