using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Models.DTO;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : BaseController<CartItem, CartItemModel, CartItemDTO, int>
    {
        public CartItemsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(CartItem entity)
        {
            return entity.Id;
        }

        protected override IQueryable<CartItem> IncludeRelatedEntities(IQueryable<CartItem> query)
        {
            return query.Include(e => e.Cart).Include(e => e.Book).ThenInclude(b => b.PosterImages);
        }

        [HttpPost]
        public override async Task<ActionResult<CartItemModel>> PostEntity(CartItemModel model)
        {
            try
            {
                if (_context.Set<CartItem>() == null)
                {
                    return Problem($"Entity set '{typeof(CartItem).Name}' is null.");
                }

                CartItem? entity = await _context.Set<CartItem>()
                    .FirstOrDefaultAsync(e => e.BookId == model.BookId && e.CartId == model.CartId);

                if (entity == null)
                {
                    entity = _mapper.Map<CartItem>(model);
                    _context.Set<CartItem>().Add(entity);
                }
                else
                {
                    entity.Quantity += model.Quantity;
                    entity.CreatedAt = DateTime.Now;
                    _context.Entry(entity).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEntity), new { id = GetEntityId(entity) }, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("IntoMoney")]
        public async Task<ActionResult> IntoMoney([FromQuery] int[] cartItemIds)
        {
            if (cartItemIds == null || cartItemIds.Length == 0)
            {
                return Ok(0);
            }

            // Get the CartItems from the database
            var cartItems = await _context.CartItems
                .Where(ci => cartItemIds.Contains(ci.Id))
                .Include(ci => ci.Book) // Include the related Book entity
                .ToListAsync();

            if (cartItems == null || cartItems.Count == 0)
            {
                return NotFound("No cart items found for the given IDs.");
            }

            // Calculate the total money
            int totalMoney = 0;
            foreach (var item in cartItems)
            {
                var price = item.Book.Price;
                var discountPercentage = item.Book.DiscountPercentage;
                var currentPrice = price - price * discountPercentage / 100;
                var quantity = item.Quantity;
                totalMoney += currentPrice * quantity;
            }

            return Ok(totalMoney);
        }
    }
}
