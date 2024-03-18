using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using mpp_csharp_stolniceanudenisa_final.model;
using mpp_csharp_stolniceanudenisa_final.persistence;

namespace mpp_csharp_stolniceanudenisa_final.database;

public class UserDbRepository:IUserRepository
{
    private static readonly ILog log = LogManager.GetLogger("UserDbRepository");
    private IDictionary<String, string> props;

    public UserDbRepository(IDictionary<String, string> props)
    {
        log.Info("Creating UserDbRepository ");
        this.props = props;
    }

 
    public User findOne(long id)
    {
        log.InfoFormat("Entering UserDbRepository findOne with value {0}", id);
        IDbConnection con = DBUtils.getConnection(props);
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM users WHERE user_id = @id";
            IDbDataParameter paramId = comm.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            comm.Parameters.Add(paramId);
            using (var dataR = comm.ExecuteReader())
            {
                if (dataR.Read())
                {
                    // long idUser = dataR.GetInt64(0);
                    String username = dataR.GetString(1);
                    String password = dataR.GetString(2);
                    //User user = new User(idUser, username, password);
                    User user = new User(username, password);
                    log.InfoFormat("Exiting UserDbRepository findOne with value {0}", user);
                    return user;
                }
            }
        }
        log.InfoFormat("Exiting UserDbRepository findOne with value {0}", null);
        return null;
    }

    public IEnumerable<User> GetAll()
    {
        log.Info("Entering UserDbRepository GetAll Users");
        IDbConnection con = DBUtils.getConnection(props);
        IList<User> users = new List<User>();
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM users";

            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                   // long id = dataR.GetInt64(0);
                    String username = dataR.GetString(1);
                    String password = dataR.GetString(2);
                    //User user = new User(id, username, password);
                    User user = new User( username, password);
                    users.Add(user);
                }
            }
        }
        log.Info("Exiting UserDbRepository GetAll Users");
        return users;
    }

    public void Add(User entity)
    {
        log.InfoFormat("Entering UserDbRepository Add value {0}", entity);
        using(IDbConnection connection = DBUtils.getConnection(props))
        {
            log.InfoFormat("UserDbRepository Add opened connection to database {0}", connection);
            using (IDbCommand command = connection.CreateCommand())
            {
                //command.CommandText = "INSERT INTO users(user_id, username, password) VALUES (@id, @username, @password)";
                
                command.CommandText = "INSERT INTO users( username, password) VALUES ( @username, @password)";
            
                // IDbDataParameter paramId = command.CreateParameter();   
                // paramId.ParameterName = "@id";
                // paramId.Value = entity.id;
                // command.Parameters.Add(paramId);

                IDbDataParameter paramUsername = command.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = entity.Username;
                command.Parameters.Add(paramUsername);

                IDbDataParameter paramPassword = command.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = entity.Password;
                command.Parameters.Add(paramPassword);
                
                var result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("UserDbRepository Add failed!");
                    throw new Exception("No user added !");
                }
            }
        }
        log.InfoFormat("Exiting UserDbRepository Add with value {0}", entity);
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(long id)
    {
        log.InfoFormat("Entering UserDbRepository Delete with value {0}", id);
        IDbConnection con = DBUtils.getConnection(props);
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "DELETE FROM users WHERE user_id=@id";
            IDbDataParameter paramId = comm.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            comm.Parameters.Add(paramId);
            var result = comm.ExecuteNonQuery();
            if (result == 0)
            {
                log.Error("UserDbRepository Delete failed!");
                throw new Exception("No user deleted !");
            }
        }
        log.InfoFormat("Exiting UserDbRepository Delete with value {0}", id);
    }

    public bool ExistsUser(string username, string password)
    {
        log.InfoFormat("Entering ExistsUser with username {0}", username);
        IDbConnection con = DBUtils.getConnection(props);
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM users WHERE username=@username AND password=@password";
            IDbDataParameter paramUsername = comm.CreateParameter();
            paramUsername.ParameterName = "@username";
            paramUsername.Value = username;
            comm.Parameters.Add(paramUsername);
            IDbDataParameter paramPassword = comm.CreateParameter();
            paramPassword.ParameterName = "@password";
            paramPassword.Value = password;
            comm.Parameters.Add(paramPassword);

            using (var dataR = comm.ExecuteReader())
            {
                if (dataR.Read())
                {
                    log.InfoFormat("Exiting ExistsUser with value {0}", true);
                    return true;
                }
            }
        }
        log.InfoFormat("Exiting ExistsUser with value {0}", false);
        return false;
    }

    public User FindUserByCredentials(string username, string password)
    {
        log.InfoFormat("Entering FindUserByCredentials with username {0}", username);
        IDbConnection con = DBUtils.getConnection(props);

        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM users WHERE username=@username AND password=@password";
            IDbDataParameter paramUsername = comm.CreateParameter();
            paramUsername.ParameterName = "@username";
            paramUsername.Value = username;
            comm.Parameters.Add(paramUsername);
            IDbDataParameter paramPassword = comm.CreateParameter();
            paramPassword.ParameterName = "@password";
            paramPassword.Value = password;
            comm.Parameters.Add(paramPassword);

            using (var dataR = comm.ExecuteReader())
            {
                if (dataR.Read())
                {
                   // long idUser = dataR.GetInt64(0);
                    String usernameUser = dataR.GetString(1);
                    String passwordUser = dataR.GetString(2);
                    //User user = new User(idUser, usernameUser, passwordUser);
                    User user = new User(usernameUser, passwordUser);
                    
                    log.InfoFormat("Exiting FindUserByCredentials with value {0}", user);
                    return user;
                }
            }
        }
        log.InfoFormat("Exiting FindUserByCredentials with value {0}", null);
        return null;
    }
}