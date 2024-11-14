using BancoMasterBack.Domain.Entities;
using BancoMasterBack.Domain.Interfaces.Repositories;
using Moq;

namespace BancoMasterBack.xUnitTest.ServicesTests
{
    public class CalculateDijkstraServiceTests
    {
        private readonly CalculateDijkstraService _calculateDijkstraService;
        private readonly Mock<IRouteRepository> _routeRepositoryMock;

        public CalculateDijkstraServiceTests()
        {
            _routeRepositoryMock = new Mock<IRouteRepository>();
            _calculateDijkstraService = new CalculateDijkstraService(_routeRepositoryMock.Object);
        }

        [Fact]
        public async Task WhenDijkstraAsyncSuccessReturnObject()
        {
            // arrange
            var path = new List<string>
            {
                "GRU",
                "BRC",
                "SCL",
                "ORL",
                "CDG"
            };

            _routeRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Route>
                {
                    new Route { Origin = "GRU", Destination = "BRC", Value = 10m },
                    new Route { Origin = "BRC", Destination = "SCL", Value = 5m },
                    new Route { Origin = "GRU", Destination = "CDG", Value = 75m },
                    new Route { Origin = "GRU", Destination = "SCL", Value = 20m },
                    new Route { Origin = "GRU", Destination = "ORL", Value = 56m },
                    new Route { Origin = "ORL", Destination = "CDG", Value = 5m },
                    new Route { Origin = "SCL", Destination = "ORL", Value = 20m }
                });

            // act 
            var result = await _calculateDijkstraService.DijkstraAsync("GRU", "CDG");

            // assert
            Assert.NotNull(result);
            Assert.Equal(40, result.Value);
            Assert.NotEmpty(result.Path);
            Assert.Equal(path, result.Path);

        }
    }
}
