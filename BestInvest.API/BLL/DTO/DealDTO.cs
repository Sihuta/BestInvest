namespace BestInvest.API.BLL.DTO
{
    public class DealDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ProjectId { get; set; }
        public decimal MoneyCapital { get; set; }
        public string State { get; set; }
    }
}
