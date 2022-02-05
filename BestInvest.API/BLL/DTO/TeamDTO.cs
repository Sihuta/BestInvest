namespace BestInvest.API.BLL.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TeamMemberDTO> TeamMembers { get; set; }
    }
}
