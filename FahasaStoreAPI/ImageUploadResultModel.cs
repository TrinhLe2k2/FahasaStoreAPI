namespace FahasaStoreAPI
{
    public class ImageUploadResultModel
    {
        public ImageUploadResultModel(string url, string? publicId)
        {
            Url = url;
            PublicId = publicId;
        }

        public string Url { get; set; }
        public string? PublicId { get; set; }
    }
}
