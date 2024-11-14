namespace BancoMasterBack.Domain.Entities
{
    public class Route
    {
        public int Id { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public decimal Value { get; set; }
    }
}
