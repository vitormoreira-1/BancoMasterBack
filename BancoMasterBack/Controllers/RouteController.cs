using AutoMapper;
using BancoMasterBack.Domain.Entities;
using BancoMasterBack.Domain.Interfaces.Services;
using BancoMasterBack.Webapi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BancoMasterBack.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly ICalculateDijkstraService _calculateDijkstraService;
        private readonly IMapper _mapper;

        public RouteController(
            IRouteService routeService, 
            ICalculateDijkstraService calculateDijkstraService, 
            IMapper mapper)
        {
            _routeService = routeService;
            _calculateDijkstraService = calculateDijkstraService;

            _mapper = mapper;
        }

        // GET /api/routes
        [HttpGet]
        public async Task<IActionResult> GetAllRoutesAsync()
        {
            var routes = await _routeService.GetAllRoutesAsync();
            return Ok(routes);
        }

        // POST /api/routes
        [HttpPost]
        public async Task<IActionResult> CreateRouteAsync(RouteViewModel route)
        {
            var result = await _routeService.AddOrUpdateRouteAsync(_mapper.Map<Domain.Entities.Route>(route));
            return Ok(result);
        }

        // DELETE /api/routes/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRoute(int id)
        {
            _routeService.DeleteRoute(id);
            return NoContent();
        }

        // GET /api/routes
        [HttpGet("{origin}/{destination}")]
        public async Task<IActionResult> GetLowestCostRouteAsync(string origin, string destination)
        {
            var routes = await _calculateDijkstraService.DijkstraAsync(origin, destination);
            return Ok(routes);
        }
    }
}
