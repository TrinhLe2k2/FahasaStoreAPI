namespace FahasaStoreAPI.Models
{
    public class FlashSaleModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
