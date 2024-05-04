namespace FahasaStoreAPI.Models.FormModels
{
    public class CategoryForm
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
