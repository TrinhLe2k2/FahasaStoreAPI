using AutoMapper;
using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookRecommendationController : ControllerBase
    {
        private readonly FahasaStoreDBContext _context;
        private readonly IMapper _mapper;

        public BookRecommendationController(FahasaStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("FindSimilarBooks/{id}")]
        public async Task<ActionResult> FindSimilarBooks(int id)
        {
            var currentBook = await _context.Books.FindAsync(id);
            if(currentBook == null)
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
                  .Include(e => e.BooksPartners)
                .FirstOrDefaultAsync(e => e.BookId == itemId);
                if (book != null)
                {
                    // Kiểm tra xem cuốn sách đã tồn tại trong danh sách result chưa
                    if (!result.Contains(book))
                    {
                        result.Add(book);
                    }
                }
            }
            return Ok(_mapper.Map<List<Book>>(result));
        }
    }

}
