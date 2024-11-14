using BancoMasterBack.Domain.Entities;

namespace BancoMasterBack.Domain.Interfaces.Services
{
    public interface IRouteService
    {
        Task<IEnumerable<Route>> GetAllRoutesAsync();

        Task<Route> AddOrUpdateRouteAsync(Route route);

        void DeleteRoute(int id);
    }
}
