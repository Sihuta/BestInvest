namespace BestInvest.API.Entities
{
    public class InvestorCategory
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public int CategoryId { get; set; }

        //
        public Account Account { get; set; }
        public Category Category { get; set; }
    }
}
