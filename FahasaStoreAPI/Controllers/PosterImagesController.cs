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
    public class PosterImagesController : BaseController<PosterImage, PosterImageModel, PosterImageDTO, int>
    {
        public PosterImagesController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(PosterImage entity)
        {
            return entity.Id;
        }

        protected override IQueryable<PosterImage> IncludeRelatedEntities(IQueryable<PosterImage> query)
        {
            return query.Include(e => e.Book);
        }
    }
}
