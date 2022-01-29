using System.ComponentModel.DataAnnotations;

namespace BestInvest.API.DAL.Entities
{
    public class Chat
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        //
        public Account Account { get; set; }
        public Project Project { get; set; }
        public List<Message> Messages { get; set; }
    }
}
