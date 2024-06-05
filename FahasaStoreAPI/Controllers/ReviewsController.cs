using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseController<Review, ReviewModel, int>
    {
        public ReviewsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Review entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Review> IncludeRelatedEntities(IQueryable<Review> query)
        {
            return query;
        }
    }
}
