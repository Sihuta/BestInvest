using System.ComponentModel.DataAnnotations;

namespace BestInvest.API.DAL.Entities
{
    public class TeamMember
    {
        public int Id { get; set; }

        public int TeamId { get; set; }
        public int AccountId { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public bool IsLeader { get; set; }

        //
        public Team Team { get; set; }
        public Account Account { get; set; }
    }
}
