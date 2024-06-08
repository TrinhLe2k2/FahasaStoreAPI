using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashSaleBooksController : BaseController<FlashSaleBook, FlashSaleBookModel, int>
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
        public async Task<ActionResult<IEnumerable<FlashSaleBook>>> GetFlashSaleBooksToday(int limited = 20)
        {
            var today = DateTime.Today;
            var flashSaleBooks = await _context.FlashSaleBooks.Include(oi => oi.FlashSale).Include(f => f.Book)
                                              .Where(oi => oi.FlashSale.StartDate <= today && oi.FlashSale.EndDate >= today)
                                              .Take(limited)
                                              .ToListAsync();
            return flashSaleBooks;
        }
    }
}
