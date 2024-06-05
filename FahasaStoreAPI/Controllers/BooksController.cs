using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using FahasaStoreAPI.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FahasaStoreAPI.Models;
using X.PagedList;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController<Book, BookModel, int>
    {
        public BooksController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Book entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Book> IncludeRelatedEntities(IQueryable<Book> query)
        {
            return query
                .Include(b => b.Author)
                .Include(b => b.CoverType)
                .Include(b => b.Dimension)
                .Include(b => b.Subcategory)
                .Include(b => b.BookPartners)
                .Include(b => b.CartItems)
                .Include(b => b.Favourites)
                .Include(b => b.FlashSaleBooks)
                .Include(b => b.OrderItems)
                .Include(b => b.PosterImages)
                .Include(b => b.Reviews);
        }

        [HttpGet]
        [Authorize] // Cần xác thực cho phương thức này
        public override async Task<ActionResult<IEnumerable<Book>>> GetEntities()
        {
            return await base.GetEntities();
        }

        [HttpGet("SimilarBooks/{id}")]
        public async Task<ActionResult> SimilarBooks(int id)
        {
            var currentBook = await _context.Books.FindAsync(id);
            if (currentBook == null)
            {
                return BadRequest();
            }
            var res = new BookRecommendationSystem().FindSimilarBooks(currentBook, 10);
            var result = new List<Book>();
            var book = new Book();
            foreach (var itemId in res)
            {
                book = await _context.Books
                .Include(e => e.Author)
                  .Include(e => e.CoverType)
                  .Include(e => e.Dimension)
                  .Include(e => e.Subcategory)
                  .Include(e => e.CartItems)
                  .Include(e => e.FlashSaleBooks)
                  .Include(e => e.OrderItems)
                  .Include(e => e.PosterImages)
                  .Include(e => e.Reviews)
                  .Include(e => e.BookPartners)
                .FirstOrDefaultAsync(e => e.Id == itemId);
                if (book != null)
                {
                    // Kiểm tra xem cuốn sách đã tồn tại trong danh sách result chưa
                    if (!result.Contains(book))
                    {
                        result.Add(book);
                    }
                }
            }
            return Ok(result);
        }

        // Xu hướng mua sắm trong ngày
        [HttpGet("trending/daily")]
        public async Task<ActionResult<IEnumerable<Book>>> GetDailyTrendingBooks()
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(1);

            return await GetTrendingBooks(startDate, endDate);
        }

        // Xu hướng mua sắm trong tháng
        [HttpGet("trending/monthly")]
        public async Task<ActionResult<IEnumerable<Book>>> GetMonthlyTrendingBooks()
        {
            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endDate = startDate.AddMonths(1);

            return await GetTrendingBooks(startDate, endDate);
        }

        // Xu hướng mua sắm trong năm
        [HttpGet("trending/yearly")]
        public async Task<ActionResult<IEnumerable<Book>>> GetYearlyTrendingBooks()
        {
            var startDate = new DateTime(DateTime.Today.Year, 1, 1);
            var endDate = startDate.AddYears(1);

            return await GetTrendingBooks(startDate, endDate);
        }

        private async Task<ActionResult<IEnumerable<Book>>> GetTrendingBooks(DateTime startDate, DateTime endDate)
        {
            var trendingBooks = await _context.OrderItems.Include(oi => oi.Order)
                                              .Where(oi => oi.Order.CreatedAt >= startDate && oi.Order.CreatedAt < endDate)
                                              .GroupBy(oi => oi.BookId)
                                              .OrderByDescending(g => g.Count())
                                              .Select(g => g.Key)
                                              .Take(10)
                                              .ToListAsync();

            var books = await _context.Books
                                      .Where(b => trendingBooks.Contains(b.Id))
                                      .ToListAsync();

            return books;
        }

        // Method to get top 10 best-selling books by subcategory
        [HttpGet("TopSellingBySubcategory/{subcategoryId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetTopSellingBooksBySubcategory(int subcategoryId)
        {
            // Assuming you have an OrderDetails entity that tracks sales data
            var topSellingBooks = await _context.OrderItems
                .Where(oi => oi.Book.SubcategoryId == subcategoryId)
                .GroupBy(oi => oi.Book)
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Select(g => g.Key)
                .Take(10)
                .ToListAsync();

            return Ok(topSellingBooks);
        }

        // Method to get top 10 best-selling books by category
        [HttpGet("TopSellingByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetTopSellingBooksByCategory(int categoryId)
        {
            // Assuming you have an OrderDetails entity that tracks sales data
            var topSellingBooks = await _context.OrderItems
                .Include(e => e.Book)
                    .ThenInclude(e => e.Subcategory)
                        .ThenInclude(e => e.Category)
                .Where(od => od.Book.Subcategory.CategoryId == categoryId)
                .GroupBy(od => od.Book)
                .OrderByDescending(g => g.Sum(od => od.Quantity))
                .Select(g => g.Key)
                .Take(10)
                .ToListAsync();

            return Ok(topSellingBooks);
        }

        // Method to get filtered and sorted books with pagination
        [HttpGet("Filter")]
        public async Task<ActionResult<PaginatedResponse<Book>>> GetFilteredBooks([FromQuery] BookFilterOptions filterOptions, int page = 1, int size = 10)
        {
            if (_context.Set<Book>() == null)
            {
                return NotFound();
            }

            var query = IncludeRelatedEntities(_context.Set<Book>()).AsQueryable();

            // Filtering by category and subcategory
            if (filterOptions.CategoryId.HasValue)
            {
                query = query.Where(b => b.Subcategory.CategoryId == filterOptions.CategoryId);
            }
            if (filterOptions.SubcategoryId.HasValue)
            {
                query = query.Where(b => b.SubcategoryId == filterOptions.SubcategoryId);
            }

            // Filtering by author
            if (filterOptions.AuthorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == filterOptions.AuthorId);
            }

            // Filtering by price range
            if (filterOptions.MinPrice.HasValue)
            {
                query = query.Where(b => (b.Price - b.Price * b.DiscountPercentage / 100) >= filterOptions.MinPrice);
            }
            if (filterOptions.MaxPrice.HasValue)
            {
                query = query.Where(b => (b.Price - b.Price * b.DiscountPercentage / 100) <= filterOptions.MaxPrice);
            }

            // Filtering by partner
            if (filterOptions.PartnerId.HasValue)
            {
                query = query.Where(b => b.BookPartners.Any(bp => bp.PartnerId == filterOptions.PartnerId));
            }

            // Filtering by cover type
            if (!string.IsNullOrEmpty(filterOptions.CoverType))
            {
                query = query.Where(b => b.CoverType.TypeName == filterOptions.CoverType);
            }

            // Filtering by flash sale
            if (filterOptions.FlashSale)
            {
                query = query.Where(b => b.FlashSaleBooks.Any());
            }

            // Sorting
            switch (filterOptions.SortBy?.ToLower())
            {
                case nameof(Book.Price):
                    query = query.OrderBy(b => (b.Price - b.Price * b.DiscountPercentage / 100));
                    break;
                case nameof(Book.Name):
                    query = query.OrderBy(b => b.Name);
                    break;
                case nameof(Book.Reviews):
                    query = query.OrderByDescending(b => b.Reviews.Count);
                    break;
                case nameof(Book.CreatedAt):
                    query = query.OrderByDescending(b => b.CreatedAt);
                    break;
                default:
                    query = query.OrderBy(b => b.Id); // Default sort by BookId
                    break;
            }

            // Pagination
            var pagedBooks = await query.ToPagedListAsync(page, size);

            // Prepare paginated response
            var response = new PaginatedResponse<Book>
            {
                Items = pagedBooks.ToList(),
                PageNumber = pagedBooks.PageNumber,
                PageSize = pagedBooks.PageSize,
                TotalItemCount = pagedBooks.TotalItemCount,
                PageCount = pagedBooks.PageCount,
                HasNextPage = pagedBooks.HasNextPage,
                HasPreviousPage = pagedBooks.HasPreviousPage,
                IsFirstPage = pagedBooks.IsFirstPage,
                IsLastPage = pagedBooks.IsLastPage
            };

            return Ok(response);
        }


    }
}
