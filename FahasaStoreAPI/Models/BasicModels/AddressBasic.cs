namespace FahasaStoreAPI.Models.BasicModels
{
    public class AddressBasic
    {
        public int AddressId { get; set; }
        public string ReceiverName { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Ward { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public bool DefaultAddress { get; set; }
        public bool Active { get; set; }
    }
}
