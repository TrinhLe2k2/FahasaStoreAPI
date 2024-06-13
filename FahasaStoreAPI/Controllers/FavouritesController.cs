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
    public class FavouritesController : BaseController<Favourite, FavouriteModel, FavouriteDTO, int>
    {
        public FavouritesController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Favourite entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Favourite> IncludeRelatedEntities(IQueryable<Favourite> query)
        {
            return query.Include(e => e.Book).Include(e => e.User);
        }
    }
}
