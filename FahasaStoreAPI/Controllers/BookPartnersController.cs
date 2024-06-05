using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookPartnersController : BaseController<BookPartner, BookPartnerModel, int>
    {
        public BookPartnersController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(BookPartner entity)
        {
            return entity.Id;
        }

        protected override IQueryable<BookPartner> IncludeRelatedEntities(IQueryable<BookPartner> query)
        {
            return query;
        }
    }
}
