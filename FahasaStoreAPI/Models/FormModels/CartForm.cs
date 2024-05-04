namespace FahasaStoreAPI.Models.FormModels
{
    public class CartForm
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
