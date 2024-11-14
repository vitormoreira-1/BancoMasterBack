namespace BancoMasterBack.Domain.Entities
{
    public class Route
    {
        public int Id { get; set; }

        public required string Origin { get; set; }

        public required string Destination { get; set; }

        public decimal Value { get; set; }
    }
}
