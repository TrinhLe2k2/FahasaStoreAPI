using AutoMapper;
using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : BaseController<Subcategory, SubcategoryModel, int>
    {
        public SubcategoriesController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Subcategory entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Subcategory> IncludeRelatedEntities(IQueryable<Subcategory> query)
        {
            return query.Include(e => e.Books).Include(e => e.Category);
        }
    }
}
