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
    public class OrderItemsController : BaseController<OrderItem, OrderItemModel, OrderItemDTO, int>
    {
        public OrderItemsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(OrderItem entity)
        {
            return entity.Id;
        }

        protected override IQueryable<OrderItem> IncludeRelatedEntities(IQueryable<OrderItem> query)
        {
            return query.Include(e => e.Book).Include(e => e.Order);
        }
    }
}
