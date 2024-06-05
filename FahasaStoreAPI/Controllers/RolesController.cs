using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<AspNetRole, AspNetRoleModel, string>
    {
        public RolesController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override string GetEntityId(AspNetRole entity)
        {
            return entity.Id;
        }

        protected override IQueryable<AspNetRole> IncludeRelatedEntities(IQueryable<AspNetRole> query)
        {
            return query;
        }
    }
}
