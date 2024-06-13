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
    public class DimensionsController : BaseController<Dimension, DimensionModel, DimensionDTO, int>
    {
        public DimensionsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Dimension entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Dimension> IncludeRelatedEntities(IQueryable<Dimension> query)
        {
            return query.Include(e => e.Books);
        }
    }
}
