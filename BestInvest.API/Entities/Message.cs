using System.ComponentModel.DataAnnotations;

namespace BestInvest.API.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public int? ChatId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        //
        public Account Account { get; set; }
        public Chat Chat { get; set; }
    }
}
