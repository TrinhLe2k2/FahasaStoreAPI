using AutoMapper;
using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Models;
using FahasaStoreAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverTypesController : BaseController<CoverType, CoverTypeModel, CoverTypeDTO, int>
    {
        public CoverTypesController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(CoverType entity)
        {
            return entity.Id;
        }

        protected override IQueryable<CoverType> IncludeRelatedEntities(IQueryable<CoverType> query)
        {
            return query.Include(e => e.Books);
        }
    }
}
