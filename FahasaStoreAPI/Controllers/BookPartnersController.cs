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
    public class BookPartnersController : BaseController<BookPartner, BookPartnerModel, BookPartnerDTO, int>
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
            return query.Include(b => b.Book).Include(b => b.Partner);
        }
    }
}
