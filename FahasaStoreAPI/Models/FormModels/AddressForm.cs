namespace FahasaStoreAPI.Models.FormModels
{
    public class AddressForm
    {
        public int AddressId { get; set; }
        public int? UserId { get; set; }
        public string ReceiverName { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Ward { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public bool DefaultAddress { get; set; }
        public bool Active { get; set; }
    }
}
