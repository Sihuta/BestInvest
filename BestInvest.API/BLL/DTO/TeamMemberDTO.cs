namespace BestInvest.API.BLL.DTO
{
    public class TeamMemberDTO
    {
        public int Id { get; set; }

        public int TeamId { get; set; }
        public int AccountId { get; set; }
        public string Position { get; set; }
        public bool IsLeader { get; set; }
    }
}
