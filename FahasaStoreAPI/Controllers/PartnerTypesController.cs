using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerTypesController : BaseController<PartnerType, PartnerTypeModel, int>
    {
        public PartnerTypesController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(PartnerType entity)
        {
            return entity.Id;
        }

        protected override IQueryable<PartnerType> IncludeRelatedEntities(IQueryable<PartnerType> query)
        {
            return query.Include(e => e.Partners);
        }
    }
}
