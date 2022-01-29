using System.ComponentModel.DataAnnotations;

namespace BestInvest.API.DAL.Entities
{
    public class AccountInfo
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int WorkingExperience { get; set; }
        public string LinkedIn { get; set; }

        //
        public Account Account { get; set; }
    }
}
