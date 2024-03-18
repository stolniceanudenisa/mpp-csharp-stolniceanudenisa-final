using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using mpp_csharp_stolniceanudenisa_final.model;
using mpp_csharp_stolniceanudenisa_final.persistence;

namespace mpp_csharp_stolniceanudenisa_final.database;

public class ClientDbRepository:IClientRepository
{
        private static readonly ILog log = LogManager.GetLogger("ClientDbRepository");
        private IDictionary<String, string> props;

        public ClientDbRepository(IDictionary<String, string> props)
        {
            log.Info("Creating ClientDbRepository");
            this.props = props;
        }

        public Client findOne(long id)
        {
            log.InfoFormat("Entering findOne Client with value {0}", id);
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Clients WHERE client_id = @id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        //long idClient = dataR.GetInt64(0);
                        String usernameClient = dataR.GetString(1);
                        String passwordClient = dataR.GetString(2);
                        Client client = new Client(usernameClient, passwordClient);
                        log.InfoFormat("Exiting findOne Client with value {0}", client);
                        return client;
                    }
                }
            }
            log.InfoFormat("Exiting findOne Client with value {0}", null);
            return null;
        }

        public IEnumerable<Client> GetAll()
        {
            
            log.Info("Entering findAll Client");
            IDbConnection con = DBUtils.getConnection(props);
            IList<Client> clients = new List<Client>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM clients";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        //long idClient = dataR.GetInt64(0);
                        String usernameClient = dataR.GetString(1);
                        String passwordClient = dataR.GetString(2);
                        Client client = new Client( usernameClient, passwordClient);
                        clients.Add(client);
                    }
                }
            }
            log.InfoFormat("Exiting findAll Client with value {0}", clients);
            return clients;
        }

        public void Add(Client entity)
        {
            log.InfoFormat("Entering ClientDbRepository Add value {0}", entity);
            using(IDbConnection connection = DBUtils.getConnection(props))
            {
                log.InfoFormat("ClientDbRepository Add opened connection to database {0}", connection);
                using (IDbCommand command = connection.CreateCommand())
                {
                     
                
                    command.CommandText = "INSERT INTO clients( client_name, address) VALUES ( @name, @address)";
             

                    IDbDataParameter paramUsername = command.CreateParameter();
                    paramUsername.ParameterName = "@name";
                    paramUsername.Value = entity.Name;
                    command.Parameters.Add(paramUsername);

                    IDbDataParameter paramPassword = command.CreateParameter();
                    paramPassword.ParameterName = "@address";
                    paramPassword.Value = entity.Address;
                    command.Parameters.Add(paramPassword);
                
                    var result = command.ExecuteNonQuery();
                    if (result == 0)
                    {
                        log.Error("ClientDbRepository Add failed!");
                        throw new Exception("No client added !");
                    }
                }
            }
            log.InfoFormat("Exiting ClientDbRepository Add with value {0}", entity);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Update(Client entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            log.InfoFormat("Entering ClientDbRepository Delete with value {0}", id);
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "DELETE FROM clients WHERE client_id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);
                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("ClientDbRepository Delete failed!");
                    throw new Exception("No client deleted !");
                }
            }
            log.InfoFormat("Exiting ClientDbRepository Delete with value {0}", id);
        }
}