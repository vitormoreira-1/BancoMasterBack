namespace BancoMasterBack.Webapi.DTOs
{
    public class RouteViewModel
    {
        public int Id { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public decimal Value { get; set; }
    }
}
