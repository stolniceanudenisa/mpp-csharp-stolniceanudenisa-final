using mpp_csharp_stolniceanudenisa_final.model;
 

namespace ClientCsharp.persistence
{
    public interface IClientRepository: IRepository<long, Client>
    {
        long GetLowestAvailableId();

    }
}