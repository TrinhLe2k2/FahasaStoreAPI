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
    public class PaymentsController : BaseController<Payment, PaymentModel, PaymentDTO, int>
    {
        public PaymentsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Payment entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Payment> IncludeRelatedEntities(IQueryable<Payment> query)
        {
            return query.Include(e => e.Order);
        }
    }
}
