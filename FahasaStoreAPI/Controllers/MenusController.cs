using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using FahasaStoreAPI.Services;
using FahasaStoreAPI.Models.DTO;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : BaseController<Menu, MenuModel, MenuDTO, int>
    {
        public MenusController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Menu entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Menu> IncludeRelatedEntities(IQueryable<Menu> query)
        {
            return query;
        }
    }
}
