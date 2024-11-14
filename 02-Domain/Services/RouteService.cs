using BancoMasterBack.Domain.Entities;
using BancoMasterBack.Domain.Interfaces.Repositories;
using BancoMasterBack.Domain.Interfaces.Services;

namespace BancoMasterBack.Domain.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<IEnumerable<Route>> GetAllRoutesAsync() =>
            await _routeRepository.GetAllAsync();

        public async Task<Route> AddOrUpdateRouteAsync(Route route)
        {
            var _route = await _routeRepository.FindAsync(route);

            if (_route == null || _route.Id == 0)
            {
                return await _routeRepository.AddAsync(route);
            }

            _route.Value = route.Value;
            return await _routeRepository.UpdateAsync(_route);
        }

        public void DeleteRoute(int id) =>
            _routeRepository.Delete(id);
    }
}
