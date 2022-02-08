namespace BestInvest.API.BLL.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public int AccountInfoId { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int WorkingExperience { get; set; }
        public string LinkedIn { get; set; }
    }
}
