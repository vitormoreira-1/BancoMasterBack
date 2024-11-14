using BancoMasterBack.Domain.Interfaces.Repositories;
using BancoMasterBack.Domain.Interfaces.Services;
using BancoMasterBack.Domain.Models;

public class CalculateDijkstraService : ICalculateDijkstraService
{
    private readonly IRouteRepository _routeRepository;

    public CalculateDijkstraService(IRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }

    public async Task<DijkstraResultModel> DijkstraAsync(string origin, string destination)
    {
        var graph = await MakeGraphAsync();

        var distances = new Dictionary<string, decimal>();
        var initial = new Dictionary<string, string>();
        var notVisitedPoint = new List<string>(graph.Keys);

        foreach (var ponto in graph.Keys)
        {
            distances[ponto] = decimal.MaxValue;
            initial[ponto] = null;
        }
        distances[origin] = 0;

        while (notVisitedPoint.Count > 0)
        {
            var current = notVisitedPoint
                .OrderBy(p => distances[p])
                .First();

            notVisitedPoint.Remove(current);

            if (current == destination)
                break;

            foreach (var edge in graph[current])
            {
                var path = distances[current] + edge.Weight;
                if (path < distances[edge.Destination])
                {
                    distances[edge.Destination] = path;
                    initial[edge.Destination] = current;
                }
            }
        }

        // Exibe o caminho e a distância para o Destination
        if (distances[destination] == decimal.MaxValue)
        {
            Console.WriteLine($"Não há caminho entre {origin} e {destination}.");
        }

        var caminho = MakePath(initial, destination);
        caminho.Reverse(); // Reverse the order to start with origin first

        return new DijkstraResultModel { Path = caminho, Value = distances[destination] };
    }

    private static List<string> MakePath(Dictionary<string, string> initial, string destination)
    {
        var path = new List<string>();
        var current = destination;
        while (current != null)
        {
            path.Add(current);
            current = initial[current];
        }
        return path;
    }

    private async Task<Dictionary<string, List<Edge>>> MakeGraphAsync()
    {
        var routes = await _routeRepository.GetAllAsync();
        var edge = new Dictionary<string, List<Edge>>();

        foreach (var r in routes)
        {
            if (!edge.ContainsKey(r.Origin))
                edge[r.Origin] = new List<Edge>();

            if (!edge.ContainsKey(r.Destination))
                edge[r.Destination] = new List<Edge>();

            edge[r.Origin].Add(new Edge(r.Destination, r.Value));
        }

        return edge;
    }

    private class Edge
    {
        public string Destination { get; set; }
        public decimal Weight { get; set; }

        public Edge(string destination, decimal weight)
        {
            Destination = destination;
            Weight = weight;
        }
    }
}
