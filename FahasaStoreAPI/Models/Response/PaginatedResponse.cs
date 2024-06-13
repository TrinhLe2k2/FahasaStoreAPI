namespace FahasaStoreAPI.Models.Response
{
    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; } = null!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public int PageCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
