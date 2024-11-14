namespace BancoMasterBack.Domain.Models
{
    public  class DijkstraResultModel
    {
        public IEnumerable<string> Path { get; set; }

        public decimal Value { get; set; }
    }
}
