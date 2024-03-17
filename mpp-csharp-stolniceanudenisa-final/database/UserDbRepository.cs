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
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        log.Info("Entering GetAll Users");
        IDbConnection con = DBUtils.getConnection(props);
        IList<User> users = new List<User>();
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM users";

            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    long id = dataR.GetInt64(0);
                    String username = dataR.GetString(1);
                    String password = dataR.GetString(2);
                    User user = new User(id, username, password);
                    users.Add(user);
                }
            }
        }
        log.Info("Exiting GetAll Users");
        return users;
    }

    public void Add(User entity)
    {
        log.InfoFormat("Entering Add User value {0}", entity);
        IDbConnection con = DBUtils.getConnection(props);

        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "INSERT INTO users(user_id, username, password)  values (@id,@Username, @Password)";
            
            var paramId = comm.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = entity.id;
            comm.Parameters.Add(paramId);
            
            var paramName = comm.CreateParameter();
            paramName.ParameterName = "@username";
            paramName.Value = entity.Username;
            comm.Parameters.Add(paramName);

            var paramAddress = comm.CreateParameter();
            paramAddress.ParameterName = "@password";
            paramAddress.Value = entity.Password;
            comm.Parameters.Add(paramAddress);
 

            var result = comm.ExecuteNonQuery();
            if (result == 0)
            {
                log.Error("No user added !");
                throw new Exception("No user added !");
            }
        }
        log.InfoFormat("Succesfully added user value {0}", entity);
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
        throw new NotImplementedException();
    }

    public bool ExistsUser(string username, string password)
    {
        throw new NotImplementedException();
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
                    long idUser = dataR.GetInt64(0);
                    String usernameUser = dataR.GetString(1);
                    String passwordUser = dataR.GetString(2);
                    User user = new User(idUser, usernameUser, passwordUser);
                    log.InfoFormat("Exiting FindUserByCredentials with value {0}", user);
                    return user;
                }
            }
        }
        log.InfoFormat("Exiting FindUserByCredentials with value {0}", null);
        return null;
    }
}