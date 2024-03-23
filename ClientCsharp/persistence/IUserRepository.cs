using mpp_csharp_stolniceanudenisa_final.model;
 

namespace ClientCsharp.persistence
{
    public interface IUserRepository: IRepository<long, User>
    {
        bool ExistsUser(string username, string password);
        User FindUserByCredentials(string username, string password);
    }
}