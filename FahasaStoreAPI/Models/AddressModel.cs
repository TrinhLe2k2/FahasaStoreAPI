namespace FahasaStoreAPI.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string ReceiverName { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Ward { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public bool DefaultAddress { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
