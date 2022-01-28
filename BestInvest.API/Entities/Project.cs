using System.ComponentModel.DataAnnotations;

namespace BestInvest.API.Entities
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public int TeamId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal MoneyCapital { get; set; }

        [Required]
        public decimal StartCapital { get; set; }

        [Required]
        public int Profitability { get; set; }

        [Required]
        public int PaybackPeriod { get; set; }

        public string BusinessPlanFilePath { get; set; }

        //
        public Team Team { get; set; }
        public List<ProjectCategory> ProjectCategories { get; set; }
        public List<Deal> Deals { get; set; }
        public List<Chat> Chats { get; set; }
    }
}
