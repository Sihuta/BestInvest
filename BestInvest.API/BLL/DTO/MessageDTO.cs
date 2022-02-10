namespace BestInvest.API.BLL.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int? ChatId { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
