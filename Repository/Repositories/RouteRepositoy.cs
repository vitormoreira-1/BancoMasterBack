using BancoMasterBack.Domain.Entities;
using BancoMasterBack.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BancoMasterBack.Repository.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly AppDbContext _context;

        public RouteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Route>> GetAllAsync() =>
            await _context.Set<Route>().ToListAsync();

        public async Task<Route> FindAsync(Route route)
        {
            var result = from r in _context.Routes
                         where
                              r.Origin == route.Origin &&
                              r.Destination == route.Destination
                         select r;

            return await result.SingleOrDefaultAsync();
        }

        public async Task<Route> AddAsync(Route route)
        {
            var result = await _context.AddAsync(route);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Route> UpdateAsync(Route route)
        {
            var result = _context.Routes.Update(route);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public void Delete(int id)
        {
            var route = _context.Set<Route>().Find(id);
            if (route != null)
            {
                _context.Set<Route>().Remove(route);
                _context.SaveChanges();
            }
        }
    }
}
