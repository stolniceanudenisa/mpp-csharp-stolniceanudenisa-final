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
        throw new NotImplementedException();
    }

    public IEnumerable<Flight> GetAll()
    {
        log.Info("Entering GetAll Flights");
        IDbConnection con = DBUtils.getConnection(props);
        IList<Flight> flights = new List<Flight>();
        // using (var comm = con.CreateCommand())
        // {
        //     comm.CommandText = "SELECT * FROM flights";
        //
        //     using (var dataR = comm.ExecuteReader())
        //     {
        //         while (dataR.Read())
        //         {
        //             long id = dataR.GetInt64(0);
        //             String username = dataR.GetString(1);
        //             String password = dataR.GetString(2);
        //             
        //             
        //             Flight flight = new Flight(id, username, password);
        //            
        //             flights.Add(flight);
        //         }
        //     }
        // }
        log.Info("Exiting GetAll Flights");
        return flights;
    }

    public void Add(Flight entity)
    {
        log.InfoFormat("Entering FlightDbRepository Add value {0}", entity);
        using(IDbConnection connection = DBUtils.getConnection(props))
        {
            log.InfoFormat("FlightDbRepository Add opened connection to database {0}", connection);
            // using (IDbCommand command = connection.CreateCommand())
            // {
            //     command.CommandText = "INSERT INTO flights(user_id, username, password) VALUES (@id, @username, @password)";
            //
            //     IDbDataParameter paramId = command.CreateParameter();
            //     paramId.ParameterName = "@id";
            //     paramId.Value = entity.id;
            //     command.Parameters.Add(paramId);
            //
            //     IDbDataParameter paramUsername = command.CreateParameter();
            //     paramUsername.ParameterName = "@username";
            //     paramUsername.Value = entity.Username;
            //     command.Parameters.Add(paramUsername);
            //
            //     IDbDataParameter paramPassword = command.CreateParameter();
            //     paramPassword.ParameterName = "@password";
            //     paramPassword.Value = entity.Password;
            //     command.Parameters.Add(paramPassword);
            //     
            //     var result = command.ExecuteNonQuery();
            //     if (result == 0)
            //     {
            //         log.Error("FlightDbRepository Add failed!");
            //         throw new Exception("No flight added !");
            //     }
            // }
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
        // using (var comm = con.CreateCommand())
        // {
        //     comm.CommandText = "DELETE FROM users WHERE user_id=@id";
        //     IDbDataParameter paramId = comm.CreateParameter();
        //     paramId.ParameterName = "@id";
        //     paramId.Value = id;
        //     comm.Parameters.Add(paramId);
        //     var result = comm.ExecuteNonQuery();
        //     if (result == 0)
        //     {
        //         log.Error("FlightDbRepository Delete failed!");
        //         throw new Exception("No user deleted !");
        //     }
        // }
        log.InfoFormat("Exiting FlightDbRepository Delete with value {0}", id);
    }

    public IEnumerable<Flight> FindFlightByDestinationAndDate(string destination, DateTime dateTime)
    {
        throw new NotImplementedException();
    }
}