using mpp_csharp_stolniceanudenisa_final.model;

namespace mpp_csharp_stolniceanudenisa_final.persistence
{
    public interface IUserRepository: IRepository<long, User>
    {
        bool ExistsUser(string username, string password);
        User FindUserByCredentials(string username, string password);
    }
}