namespace FahasaStoreAPI.Models.FormModels
{
    public class SubcategoryForm
    {
        public int SubcategoryId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
