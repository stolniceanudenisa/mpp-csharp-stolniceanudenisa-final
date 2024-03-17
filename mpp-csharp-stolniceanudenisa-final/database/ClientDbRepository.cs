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
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAll()
        {
            
            log.Info("Entering findAll Client");
            IDbConnection con = DBUtils.getConnection(props);
            IList<Client> clients = new List<Client>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM Clients";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        long idClient = dataR.GetInt64(0);
                        String usernameClient = dataR.GetString(1);
                        String passwordClient = dataR.GetString(2);
                        Client client = new Client(idClient, usernameClient, passwordClient);
                        clients.Add(client);
                    }
                }
            }
            log.InfoFormat("Exiting findAll Client with value {0}", clients);
            return clients;
        }

        public void Add(Client entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
}