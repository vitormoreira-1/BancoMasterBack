namespace BancoMasterBack.Webapi.DTOs
{
    public class RouteViewModel
    {
        public required int Id { get; set; }

        public required string Origin { get; set; }

        public required string Destination { get; set; }

        public required decimal Value { get; set; }
    }
}
