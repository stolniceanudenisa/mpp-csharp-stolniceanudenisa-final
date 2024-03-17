using mpp_csharp_stolniceanudenisa_final.model;

namespace mpp_csharp_stolniceanudenisa_final.persistence
{
    public interface IClientRepository: IRepository<long, Client>
    {
        // long getLowestAvailableId();
    }
}