using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : BaseController<Cart, CartModel, int>
    {
        public CartsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Cart entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Cart> IncludeRelatedEntities(IQueryable<Cart> query)
        {
            return query.Include(e => e.User).Include(e => e.CartItems);
        }
    }
}
