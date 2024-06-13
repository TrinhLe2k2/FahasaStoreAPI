using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using FahasaStoreAPI.Models.DTO;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BaseController<Author, AuthorModel, AuthorDTO, int>
    {
        public AuthorsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Author entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Author> IncludeRelatedEntities(IQueryable<Author> query)
        {
            return query.Include(e => e.Books);
        }
    }
}
