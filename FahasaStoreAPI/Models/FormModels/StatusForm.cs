namespace FahasaStoreAPI.Models.FormModels
{
    public class StatusForm
    {
        public int StatusId { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
    }
}
