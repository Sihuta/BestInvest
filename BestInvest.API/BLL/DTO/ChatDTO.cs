namespace BestInvest.API.BLL.DTO
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public List<MessageDTO> Messages { get; set; }

        public bool IsActive { get; set; }
    }
}
