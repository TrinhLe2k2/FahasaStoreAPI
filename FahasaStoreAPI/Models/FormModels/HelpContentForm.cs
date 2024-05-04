namespace FahasaStoreAPI.Models.FormModels
{
    public class HelpContentForm
    {
        public int HelpContentId { get; set; }
        public int? HelpId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
