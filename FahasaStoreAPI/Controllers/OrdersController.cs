using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController<Order, OrderModel, int>
    {
        public OrdersController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Order entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Order> IncludeRelatedEntities(IQueryable<Order> query)
        {
            return query;
        }
    }
}
