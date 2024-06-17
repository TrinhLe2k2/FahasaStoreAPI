namespace FahasaStoreAPI.Models.DTO
{
    public class MonthlyStatisticsDTO
    {
        public MonthlyStatisticsDTO()
        {
            Top10Books = new List<BookDTO>();
        }
        public int Month { get; set; }
        public int TotalRevenue { get; set; }
        public int TotalOrdersCompleted { get; set; }
        public int TotalOrdersCancelled { get; set; }
        public List<BookDTO> Top10Books { get; set; }
        public int NewUsers { get; set; }
        public int TotalBooksSold { get; set; }
    }
}
