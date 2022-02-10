using BestInvest.API.BLL.Enums;
using System.ComponentModel.DataAnnotations;

namespace BestInvest.API.DAL.Entities
{
    public class Deal
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public int ProjectId { get; set; }

        [Required]
        public decimal MoneyCapital { get; set; }

        [Required]
        public DealState State { get; set; }

        //
        public Account Account { get; set; }
        public Project Project { get; set; }
    }
}
