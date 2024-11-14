using BancoMasterBack.Domain.Entities;

namespace BancoMasterBack.Domain.Interfaces.Repositories
{
    public interface IRouteRepository
    {
        Task<IEnumerable<Route>> GetAllAsync();

        Task<Route?> FindAsync(Route route);

        Task<Route> AddAsync(Route route);

        Task<Route> UpdateAsync(Route route);

        void Delete(int id);
    }
}
