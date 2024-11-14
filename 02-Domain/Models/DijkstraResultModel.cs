namespace BancoMasterBack.Domain.Models
{
    public  class DijkstraResultModel
    {
        public required IEnumerable<string> Path { get; set; }

        public decimal Value { get; set; }
    }
}
