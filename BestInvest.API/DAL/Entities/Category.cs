using System.ComponentModel.DataAnnotations;

namespace BestInvest.API.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //
        public List<InvestorCategory> InvestorCategories { get; set; }
        public List<ProjectCategory> ProjectCategories { get; set; }
    }
}
