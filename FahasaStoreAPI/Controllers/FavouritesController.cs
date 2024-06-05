using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : BaseController<Favourite, FavouriteModel, int>
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
            return query;
        }
    }
}
