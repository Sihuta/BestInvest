using System.ComponentModel.DataAnnotations;

namespace BestInvest.API.DAL.Entities
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //
        public List<TeamMember> TeamMembers { get; set; }
        public List<Project> Projects { get; set; }
    }
}
