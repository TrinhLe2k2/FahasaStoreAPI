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
    public class CategoriesController : BaseController<Category, CategoryModel, CategoryDTO, int>
    {
        public CategoriesController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Category entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Category> IncludeRelatedEntities(IQueryable<Category> query)
        {
            return query.Include(e => e.Subcategories);
        }
    }
}
