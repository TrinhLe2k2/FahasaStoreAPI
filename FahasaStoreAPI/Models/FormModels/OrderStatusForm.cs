namespace FahasaStoreAPI.Models.FormModels
{
    public class OrderStatusForm
    {
        public int OrderStatusId { get; set; }
        public int? OrderId { get; set; }
        public int? StatusId { get; set; }
        public DateTime OrderStatusDate { get; set; }
    }
}
