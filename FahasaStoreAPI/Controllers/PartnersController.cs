using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : BaseController<Partner, PartnerModel, int>
    {
        public PartnersController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Partner entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Partner> IncludeRelatedEntities(IQueryable<Partner> query)
        {
            return query.Include(e => e.PartnerType).Include(e => e.BookPartners);
        }
    }
}
