namespace FahasaStoreAPI.Models.BasicModels
{
    public class ReviewBasic
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
