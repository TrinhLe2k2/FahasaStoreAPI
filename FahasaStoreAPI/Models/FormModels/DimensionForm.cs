namespace FahasaStoreAPI.Models.FormModels
{
    public class DimensionForm
    {
        public int DimensionId { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Unit { get; set; } = null!;
    }
}
