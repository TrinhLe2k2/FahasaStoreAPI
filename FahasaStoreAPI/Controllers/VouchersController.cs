using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController : BaseController<Voucher, VoucherModel, int>
    {
        public VouchersController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Voucher entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Voucher> IncludeRelatedEntities(IQueryable<Voucher> query)
        {
            return query;
        }
    }
}
