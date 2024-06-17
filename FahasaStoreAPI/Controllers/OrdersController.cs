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
    public class OrdersController : BaseController<Order, OrderModel, OrderDTO, int>
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
            return query
                .Include(e => e.Address)
                .Include(e => e.PaymentMethod)
                .Include(e => e.User)
                .Include(e => e.Voucher)
                .Include(e => e.Payment)
                .Include(e => e.Reviews)
                .Include(e => e.OrderItems)
                    .ThenInclude(oi => oi.Book)
                        .ThenInclude(b => b.PosterImages)
                .Include(e => e.OrderStatuses)
                    .ThenInclude(o => o.Status);
        }
    }
}
