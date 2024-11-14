using BancoMasterBack.Domain.Entities;

namespace BancoMasterBack.Repository.Seed
{
    public static class SeedDataDbInMemory
    {
        public static void Initialize(IServiceProvider serviceProvider, AppDbContext context)
        {
            // Verifique se já existem dados na tabela para evitar duplicação
            if (!context.Routes.Any())
            {
                context.Routes.AddRange(
                    new Route { Origin = "GRU", Destination = "BRC", Value = 10m },
                    new Route { Origin = "BRC", Destination = "SCL", Value = 5m },
                    new Route { Origin = "GRU", Destination = "CDG", Value = 75m },
                    new Route { Origin = "GRU", Destination = "SCL", Value = 20m },
                    new Route { Origin = "GRU", Destination = "ORL", Value = 56m },
                    new Route { Origin = "ORL", Destination = "CDG", Value = 5m },
                    new Route { Origin = "SCL", Destination = "ORL", Value = 20m }
                );
                context.SaveChanges();
            }
        }
    }
}
