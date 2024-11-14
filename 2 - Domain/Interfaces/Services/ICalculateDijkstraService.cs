using BancoMasterBack.Domain.Models;

namespace BancoMasterBack.Domain.Interfaces.Services
{
    public interface ICalculateDijkstraService
    {
        Task<DijkstraResultModel> DijkstraAsync(string pontoInicial, string pontoDestination);
    }
}
