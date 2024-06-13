using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Models.DTO;
using FahasaStoreAPI.Models.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using X.PagedList;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashSaleBooksController : BaseController<FlashSaleBook, FlashSaleBookModel, FlashSaleBookDTO, int>
    {
        public FlashSaleBooksController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(FlashSaleBook entity)
        {
            return entity.Id;
        }

        protected override IQueryable<FlashSaleBook> IncludeRelatedEntities(IQueryable<FlashSaleBook> query)
        {
            return query.Include(e => e.FlashSale).Include(e => e.Book);
        }
        [HttpGet("GetFlashSaleBooksToday")]
        public async Task<ActionResult<PaginatedResponse<FlashSaleBookDTO>>> GetFlashSaleBooksToday(int page = 1, int size = 10)
        {
            var today = DateTime.Today;
            var flashSaleBooks = await _context.FlashSaleBooks
                                            .Include(oi => oi.FlashSale)
                                            .Include(f => f.Book)
                                                .ThenInclude(b => b.OrderItems)
                                            .Include(f => f.Book)
                                                .ThenInclude(b => b.PosterImages)
                                              .Where(oi => oi.FlashSale.StartDate <= today && oi.FlashSale.EndDate >= today)
                                              .ToListAsync();
            // Pagination
            var pagedBooks = await flashSaleBooks.ToPagedListAsync(page, size);

            // Map entities to DTOs
            //var dtoList = pagedBooks.Select(entity => _mapper.Map<FlashSaleBookDTO>(entity)).ToList();
            var dtoList = pagedBooks.Select(entity => new FlashSaleBookDTO
            {
                Id = entity.Id,
                DiscountPercentage = entity.DiscountPercentage,
                Quantity = entity.Quantity,
                CreatedAt = entity.CreatedAt,
                Book = _mapper.Map<BookModel>(entity.Book),
                Poster = entity.Book.PosterImages.FirstOrDefault()?.ImageUrl,
                FlashSale = _mapper.Map<FlashSaleModel>(entity.FlashSale),
                Sold = entity.Book.OrderItems
                    .Where(oi => oi.CreatedAt >= entity.FlashSale.StartDate && oi.CreatedAt <= entity.FlashSale.EndDate)
                    .Sum(oi => oi.Quantity) // Tính tổng số lượng đã bán trong thời gian flash sale
            }).ToList();

            // Prepare paginated response
            var response = new PaginatedResponse<FlashSaleBookDTO>
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
            return response;
        }
    }
}
