using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using FahasaStoreAPI.Services;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : BaseController<Menu, MenuModel, int>
    {
        public MenusController(FahasaStoreDBContext context, IMapper mapper, IImageUploader imageUploader) : base(context, mapper, imageUploader)
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
