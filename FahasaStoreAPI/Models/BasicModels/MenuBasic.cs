namespace FahasaStoreAPI.Models.BasicModels
{
    public class MenuBasic
    {
        public int MenuId { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
