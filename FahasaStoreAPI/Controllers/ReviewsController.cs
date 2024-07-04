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

        private async Task<bool> HasUserPurchasedBook(ReviewModel model)
        {
            var result = await _context.Orders
                .Include(e => e.User)
                .Include(e => e.OrderItems)
                .Include(e => e.OrderStatuses)
                .Where(o => o.UserId == model.UserId && o.Id == model.OrderId && o.OrderStatuses.Any(os => os.StatusId == 2))
                .SelectMany(o => o.OrderItems)
                .AnyAsync(oi => oi.BookId == model.BookId);

            return result;
        }

        [HttpPost]
        public override async Task<ActionResult<ReviewModel>> PostEntity(ReviewModel model)
        {
            var check = await HasUserPurchasedBook(model);
            if (check)
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
