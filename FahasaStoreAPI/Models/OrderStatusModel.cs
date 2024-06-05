namespace FahasaStoreAPI.Models
{
    public class OrderStatusModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int StatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
