using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using FahasaStoreAPI.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FahasaStoreAPI.Models;
using X.PagedList;
using FahasaStoreAPI.Models.DTO;
using FahasaStoreAPI.Models.Response;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController<Book, BookModel, BookDTO, int>
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
                    .ThenInclude(bp => bp.Partner)
                        .ThenInclude(bp => bp.PartnerType)
                .Include(b => b.CartItems)
                .Include(b => b.Favourites)
                .Include(b => b.FlashSaleBooks)
                    .ThenInclude(f => f.FlashSale)
                .Include(b => b.OrderItems)
                .Include(b => b.PosterImages)
                .Include(b => b.Reviews);
        }

        [HttpGet]
        //[Authorize] // Cần xác thực cho phương thức này
        public override async Task<ActionResult<IEnumerable<BookDTO>>> GetEntities()
        {
            return await base.GetEntities();
        }

        [HttpGet("SimilarBooks/{id}")]
        public async Task<ActionResult> SimilarBooks(int id, int size = 10)
        {
            var currentBook = await _context.Books.FindAsync(id);
            if (currentBook == null)
            {
                return BadRequest();
            }
            var res = new BookRecommendationSystem().FindSimilarBooks(currentBook, size);
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
            return Ok(_mapper.Map<List<BookDTO>>(result));
        }

        // Xu hướng mua sắm trong ngày
        [HttpGet("trending/daily")]
        public async Task<ActionResult<PaginatedResponse<BookDTO>>> GetDailyTrendingBooks(int page = 1, int size = 10)
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(1);
            return await GetTrendingBooks(startDate, endDate, page, size);
        }

        // Xu hướng mua sắm trong tháng
        [HttpGet("trending/monthly")]
        public async Task<ActionResult<PaginatedResponse<BookDTO>>> GetMonthlyTrendingBooks(int page = 1, int size = 10)
        {
            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endDate = startDate.AddMonths(1);

            return await GetTrendingBooks(startDate, endDate, page, size);
        }

        // Xu hướng mua sắm trong năm
        [HttpGet("trending/yearly")]
        public async Task<ActionResult<PaginatedResponse<BookDTO>>> GetYearlyTrendingBooks(int page = 1, int size = 10)
        {
            var startDate = new DateTime(DateTime.Today.Year, 1, 1);
            var endDate = startDate.AddYears(1);

            return await GetTrendingBooks(startDate, endDate, page, size);
        }

        private async Task<ActionResult<PaginatedResponse<BookDTO>>> GetTrendingBooks(DateTime startDate, DateTime endDate, int page = 1, int size = 10)
        {
            // Bước 1: Lấy danh sách các BookId của các sách bán chạy trong khoảng thời gian
            var trendingBooksQuery = _context.OrderItems.Include(oi => oi.Order)
                                              .Where(oi => oi.Order.CreatedAt >= startDate && oi.Order.CreatedAt <= endDate)
                                              .GroupBy(oi => oi.BookId)
                                              .OrderByDescending(g => g.Count())
                                              .Select(g => g.Key);

            // Bước 2: Phân trang danh sách trendingBooks
            var pagedTrendingBooks = await trendingBooksQuery.ToPagedListAsync(page, size);

            // Bước 3: Lấy danh sách các sách tương ứng với các BookId trong trang hiện tại
            var books = await _context.Books
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
                                      .Where(b => pagedTrendingBooks.Contains(b.Id))
                                      .ToListAsync();

            // Bước 4: Map các sách sang BookDTO
            var booksDTO = _mapper.Map<List<BookDTO>>(books);

            // Bước 5: Chuẩn bị phản hồi phân trang
            var response = new PaginatedResponse<BookDTO>
            {
                Items = booksDTO,
                PageNumber = pagedTrendingBooks.PageNumber,
                PageSize = pagedTrendingBooks.PageSize,
                TotalItemCount = pagedTrendingBooks.TotalItemCount,
                PageCount = pagedTrendingBooks.PageCount,
                HasNextPage = pagedTrendingBooks.HasNextPage,
                HasPreviousPage = pagedTrendingBooks.HasPreviousPage,
                IsFirstPage = pagedTrendingBooks.IsFirstPage,
                IsLastPage = pagedTrendingBooks.IsLastPage,
                StartPage = CalculateStartPage(pagedTrendingBooks.PageNumber, pagedTrendingBooks.PageCount, maxPages: 5),
                EndPage = CalculateEndPage(pagedTrendingBooks.PageNumber, pagedTrendingBooks.PageCount, maxPages: 5)
            };

            return Ok(response);
        }

        // Method to get top 10 best-selling books by subcategory
        [HttpGet("TopSellingBySubcategory/{subcategoryId}")]
        public async Task<ActionResult<PaginatedResponse<BookDTO>>> GetTopSellingBooksBySubcategory(int subcategoryId = 0, int page = 1, int size = 10)
        {
            // Kiểm tra nếu context không có Set<Book> thì trả về NotFound
            if (_context.Set<Book>() == null || _context.Set<OrderItem>() == null)
            {
                return NotFound();
            }

            // Kiểm tra sự tồn tại của categoryId nếu nó không phải là 0
            if (subcategoryId != 0)
            {
                var categoryExists = await _context.Subcategories.AnyAsync(c => c.Id == subcategoryId);
                if (!categoryExists)
                {
                    // Nếu categoryId không tồn tại, lấy ra danh sách book bán chạy mà không cần phải lọc categoryId
                    subcategoryId = 0;
                }
            }

            IQueryable<OrderItem> query = _context.OrderItems.AsQueryable();
            // Nếu categoryId khác 0, lọc theo subcategoryId
            if (subcategoryId != 0)
            {
                query = query.Where(od => od.Book.SubcategoryId == subcategoryId);
            }

            // Get top selling book IDs
            var topSellingBookIds = await query
                .GroupBy(oi => oi.BookId) // Group by BookId instead of Book
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Select(g => g.Key)
                .Take(10)
                .ToListAsync();

            // Get books with details
            var topSellingBooks = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.CoverType)
                .Include(b => b.Dimension)
                .Include(b => b.Subcategory)
                .Include(b => b.BookPartners)
                    .ThenInclude(e => e.Partner).ThenInclude(e => e.PartnerType)
                .Include(b => b.CartItems)
                .Include(b => b.Favourites)
                .Include(b => b.FlashSaleBooks)
                .Include(b => b.OrderItems)
                .Include(b => b.PosterImages)
                .Include(b => b.Reviews)
                .Where(b => topSellingBookIds.Contains(b.Id))
                .ToPagedListAsync(page, size);

            // Map entities to DTOs
            var dtoList = topSellingBooks.Select(entity => _mapper.Map<BookDTO>(entity)).ToList();

            // Prepare paginated response
            var response = new PaginatedResponse<BookDTO>
            {
                Items = dtoList,
                PageNumber = topSellingBooks.PageNumber,
                PageSize = topSellingBooks.PageSize,
                TotalItemCount = topSellingBooks.TotalItemCount,
                PageCount = topSellingBooks.PageCount,
                HasNextPage = topSellingBooks.HasNextPage,
                HasPreviousPage = topSellingBooks.HasPreviousPage,
                IsFirstPage = topSellingBooks.IsFirstPage,
                IsLastPage = topSellingBooks.IsLastPage,
                StartPage = CalculateStartPage(topSellingBooks.PageNumber, topSellingBooks.PageCount, maxPages: 5),
                EndPage = CalculateEndPage(topSellingBooks.PageNumber, topSellingBooks.PageCount, maxPages: 5)
            };

            return Ok(response);
        }

        // Method to get top 10 best-selling books by category
        [HttpGet("TopSellingByCategory/{categoryId}")]
        public async Task<ActionResult<PaginatedResponse<BookDTO>>> GetTopSellingBooksByCategory(int categoryId = 0, int page = 1, int size = 10)
        {
            // Kiểm tra nếu context không có Set<Book> thì trả về NotFound
            if (_context.Set<Book>() == null || _context.Set<OrderItem>() == null)
            {
                return NotFound();
            }

            // Kiểm tra sự tồn tại của categoryId nếu nó không phải là 0
            if (categoryId != 0)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == categoryId);
                if (!categoryExists)
                {
                    // Nếu categoryId không tồn tại, lấy ra danh sách book bán chạy mà không cần phải lọc categoryId
                    categoryId = 0;
                }
            }

            IQueryable<OrderItem> query = _context.OrderItems.AsQueryable();
            // Nếu categoryId khác 0, lọc theo categoryId
            if (categoryId != 0)
            {
                query = query.Where(od => od.Book.Subcategory.CategoryId == categoryId);
            }

            // Truy vấn để lấy danh sách ID sách bán chạy nhất
            var topSellingBookIds = await query
                .GroupBy(od => od.BookId)
                .OrderByDescending(g => g.Sum(od => od.Quantity))
                .Select(g => g.Key)
                .Take(size)
                .ToListAsync();

            // Truy vấn để lấy thông tin chi tiết của các sách bán chạy nhất
            var topSellingBooks = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.CoverType)
                .Include(b => b.Dimension)
                .Include(b => b.Subcategory)
                .Include(b => b.BookPartners)
                    .ThenInclude(e => e.Partner).ThenInclude(e => e.PartnerType)
                .Include(b => b.CartItems)
                .Include(b => b.Favourites)
                .Include(b => b.FlashSaleBooks)
                .Include(b => b.OrderItems)
                .Include(b => b.PosterImages)
                .Include(b => b.Reviews)
                .Where(b => topSellingBookIds.Contains(b.Id))
                .OrderByDescending(e => e.OrderItems.Sum(od => od.Quantity))
                .ToListAsync();

            // Phân trang
            var pagedBooks = topSellingBooks.ToPagedList(page, size);

            // Chuyển đổi entities sang DTOs
            var dtoList = pagedBooks.Select(entity => _mapper.Map<BookDTO>(entity)).ToList();

            // Chuẩn bị phản hồi phân trang
            var response = new PaginatedResponse<BookDTO>
            {
                Items = dtoList,
                PageNumber = pagedBooks.PageNumber,
                PageSize = pagedBooks.PageSize,
                TotalItemCount = pagedBooks.TotalItemCount,
                PageCount = pagedBooks.PageCount,
                HasNextPage = pagedBooks.HasNextPage,
                HasPreviousPage = pagedBooks.HasPreviousPage,
                IsFirstPage = pagedBooks.IsFirstPage,
                IsLastPage = pagedBooks.IsLastPage,
                StartPage = CalculateStartPage(pagedBooks.PageNumber, pagedBooks.PageCount, maxPages: 5),
                EndPage = CalculateEndPage(pagedBooks.PageNumber, pagedBooks.PageCount, maxPages: 5)
            };

            return Ok(response);
        }

        // Method to get filtered and sorted books with pagination
        [HttpGet("Filter")]
        public async Task<ActionResult<PaginatedResponse<BookDTO>>> GetFilteredBooks([FromQuery] BookFilterOptions filterOptions, int page = 1, int size = 10)
        {
            if (_context.Set<Book>() == null)
            {
                return NotFound();
            }

            var query = IncludeRelatedEntities(_context.Set<Book>()).AsQueryable();

            #region Filter

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

            // Filtering by PartnerType
            if (filterOptions.PartnerTypeId.HasValue)
            {
                query = query.Where(b => b.BookPartners.Any(bp => bp.Partner.PartnerTypeId == filterOptions.PartnerTypeId));
            }

            // Filtering by partner
            if (filterOptions.PartnerId.HasValue)
            {
                query = query.Where(b => b.BookPartners.Any(bp => bp.PartnerId == filterOptions.PartnerId));
            }

            // Filtering by cover type
            if (filterOptions.CoverTypeId.HasValue)
            {
                query = query.Where(b => b.CoverTypeId == filterOptions.CoverTypeId);
            }

            // Filtering by flash sale
            if (filterOptions.FlashSale)
            {
                var today = DateTime.Today;
                query = query.Where(b => b.FlashSaleBooks.Any(fsb => fsb.FlashSale.StartDate <= today && fsb.FlashSale.EndDate >= today));
            }

            // Search by book name
            if (!string.IsNullOrEmpty(filterOptions.Search))
            {
                string searchLower = filterOptions.Search.ToLower();
                query = query.Where(b => b.Name.ToLower().Contains(searchLower));
            }

            // Sorting
            switch (filterOptions.SortBy)
            {
                case nameof(Book.Price):
                    query = query.OrderBy(b => b.Price - b.Price * b.DiscountPercentage / 100);
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

            #endregion

            // Pagination
            var pagedBooks = await query.ToPagedListAsync(page, size);

            // Map entities to DTOs
            var dtoList = pagedBooks.Select(entity => _mapper.Map<BookDTO>(entity)).ToList();

            // Prepare paginated response
            var response = new PaginatedResponse<BookDTO>
            {
                Items = dtoList,
                PageNumber = pagedBooks.PageNumber,
                PageSize = pagedBooks.PageSize,
                TotalItemCount = pagedBooks.TotalItemCount,
                PageCount = pagedBooks.PageCount,
                HasNextPage = pagedBooks.HasNextPage,
                HasPreviousPage = pagedBooks.HasPreviousPage,
                IsFirstPage = pagedBooks.IsFirstPage,
                IsLastPage = pagedBooks.IsLastPage,
                StartPage = CalculateStartPage(pagedBooks.PageNumber, pagedBooks.PageCount, maxPages: 5),
                EndPage = CalculateEndPage(pagedBooks.PageNumber, pagedBooks.PageCount, maxPages: 5)
            };

            return Ok(response);
        }
        [HttpGet("HasUserPurchasedBook")]
        public async Task<bool> HasUserPurchasedBook(string userId, int bookId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Payment)
                .AnyAsync(o => o.UserId == userId
                    && o.OrderItems.Any(oi => oi.BookId == bookId)
                    && o.Payment != null);
        }
    }
}
