using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : BaseController<OrderStatus, OrderStatusModel, int>
    {
        public OrderStatusController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(OrderStatus entity)
        {
            return entity.Id;
        }

        protected override IQueryable<OrderStatus> IncludeRelatedEntities(IQueryable<OrderStatus> query)
        {
            return query;
        }
    }
}
