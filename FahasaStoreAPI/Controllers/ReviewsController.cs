using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using FahasaStoreAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseController<Review, ReviewModel, ReviewDTO, int>
    {
        public ReviewsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Review entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Review> IncludeRelatedEntities(IQueryable<Review> query)
        {
            return query.Include(e => e.Book).Include(e => e.User);
        }

        private async Task<bool> HasUserPurchasedBook(string userId, int bookId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Payment)
                .AnyAsync(o => o.UserId == userId
                    && o.OrderItems.Any(oi => oi.BookId == bookId)
                    && o.Payment != null);
        }

        [HttpPost]
        public override async Task<ActionResult<ReviewModel>> PostEntity(ReviewModel model)
        {
            if (await HasUserPurchasedBook(model.UserId, model.BookId))
            {
                return await base.PostEntity(model);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
