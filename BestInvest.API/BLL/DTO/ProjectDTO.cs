namespace BestInvest.API.BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public int TeamId { get; set; }

        public string Description { get; set; }
        public decimal MoneyCapital { get; set; }
        public decimal StartCapital { get; set; }
        public int Profitability { get; set; }
        public int PaybackPeriod { get; set; }
        public string BusinessPlanFilePath { get; set; }

        //
        public TeamDTO Team { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
