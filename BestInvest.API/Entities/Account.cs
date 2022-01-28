using System.ComponentModel.DataAnnotations;

namespace BestInvest.API.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountInfoId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        //
        public AccountInfo AccountInfo { get; set; }
        public List<InvestorCategory> InvestorCategories { get; set;}
        public List<TeamMember> TeamMembers { get; set; }
        public List<Deal> Deals { get; set; }
        public List<Chat> Chats { get; set; }
        public List<Message> Messages { get; set; }
    }
}
