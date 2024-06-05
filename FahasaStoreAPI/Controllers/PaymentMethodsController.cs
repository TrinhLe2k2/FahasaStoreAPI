using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : BaseController<PaymentMethod, PaymentMethodModel, int>
    {
        public PaymentMethodsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(PaymentMethod entity)
        {
            return entity.Id;
        }

        protected override IQueryable<PaymentMethod> IncludeRelatedEntities(IQueryable<PaymentMethod> query)
        {
            return query;
        }
    }
}
