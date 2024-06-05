namespace FahasaStoreAPI.Models
{
    public class DimensionModel
    {
        public int Id { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Unit { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
