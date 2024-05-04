namespace FahasaStoreAPI.Models.FormModels
{
    public class ReviewForm
    {
        public int ReviewId { get; set; }
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
