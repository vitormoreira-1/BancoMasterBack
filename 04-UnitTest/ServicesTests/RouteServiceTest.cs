using BancoMasterBack.Domain.Entities;
using BancoMasterBack.Domain.Interfaces.Repositories;
using BancoMasterBack.Domain.Services;
using Moq;

namespace BancoMasterBack.xUnitTest.ServicesTests
{
    public class RouteServiceTest
    {
        private readonly RouteService _routeService;
        private readonly Mock<IRouteRepository> _routeRepositoryMock;

        public RouteServiceTest()
        {
            _routeRepositoryMock = new Mock<IRouteRepository>();
            _routeService = new RouteService(_routeRepositoryMock.Object);
        }

        [Fact]
        public async Task WhenGetAllRoutesAsyncSuccessReturnListOfRoute()
        {
            // arrange
            _routeRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Route>
                {
                    new Route { Origin = "CityA", Destination = "CityB", Value = 50 },
                    new Route { Origin = "CityC", Destination = "CityD", Value = 100 }
                });

            // act 
            var routes = await _routeService.GetAllRoutesAsync();

            // assert
            Assert.NotEmpty(routes);
        }

        [Fact]
        public async Task WhenAddOrUpdateRouteAsyncFindAnyReturnUpdateEntity()
        {
            // arrange
            var updateRoute = new Route
            {
                Id = 99,
                Origin = "FGH",
                Destination = "IJK",
                Value = 150m
            };

            _routeRepositoryMock
                .Setup(r => r.FindAsync(It.IsAny<Route>()))
                .ReturnsAsync(updateRoute);

            var newUpdateRoute = new Route
            {
                Id = 99,
                Origin = "FGH",
                Destination = "IJK",
                Value = 50m
            };

            _routeRepositoryMock
                .Setup(r => r.UpdateAsync(updateRoute))
                .ReturnsAsync(newUpdateRoute);

            var route = new Route
            {
                Origin = "FGH",
                Destination = "IJK",
                Value = 50m
            };

            // act 
            var result = await _routeService.AddOrUpdateRouteAsync(route);

            // assert
            Assert.NotNull(result);
            Assert.Equal(result.Value, route.Value);
        }

        [Fact]
        public async Task WhenAddOrUpdateRouteAsyncNotFindAnyReturnAddEntity()
        {
            // arrange
            var addRoute = new Route
            {
                Id = 99,
                Origin = "ABC",
                Destination = "CDE",
                Value = 50m
            };

            _routeRepositoryMock
                .Setup(r => r.FindAsync(It.IsAny<Route>()))
                .ReturnsAsync(new Route());            

            var route = new Route
            {
                Origin = "ABC",
                Destination = "CDE",
                Value = 50m
            };

            _routeRepositoryMock
                .Setup(r => r.AddAsync(route))
                .ReturnsAsync(addRoute);

            // act 
            var result = await _routeService.AddOrUpdateRouteAsync(route);

            // assert
            Assert.NotNull(result);
            Assert.Equal(result.Value, route.Value);
        }

        [Fact]
        public void WhenDeleteSuccessReturnSuccess()
        {
            // arrange
            var routeId = 2;
            _routeRepositoryMock
                .Setup(r => r.Delete(It.IsAny<int>()));

            // act 
            _routeService.DeleteRoute(routeId);

            // assert
            _routeRepositoryMock.Verify(
                repo => repo.Delete(routeId), 
                Times.Once,
                "Expected Delete to be called once with the correct ID."
            );
        }
    }
}