using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : BaseController<CartItem, CartItemModel, int>
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
            return query.Include(e => e.Cart).Include(e => e.Book);
        }
    }
}
