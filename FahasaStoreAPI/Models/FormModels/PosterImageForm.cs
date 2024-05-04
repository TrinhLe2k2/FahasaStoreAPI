namespace FahasaStoreAPI.Models.FormModels
{
    public class PosterImageForm
    {
        public int PosterImgageId { get; set; }
        public int? BookId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool PosterDefault { get; set; }
    }
}
