namespace FahasaStoreAPI.Models.FormModels
{
    public class CartItemForm
    {
        public int CartItemId { get; set; }
        public int? CartId { get; set; }
        public int? BookId { get; set; }
        public int Quantity { get; set; }
    }
}
