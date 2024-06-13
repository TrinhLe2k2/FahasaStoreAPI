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
    public class TopicsController : BaseController<Topic, TopicModel, TopicDTO, int>
    {
        public TopicsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Topic entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Topic> IncludeRelatedEntities(IQueryable<Topic> query)
        {
            return query.Include(e => e.TopicContents);
        }
    }
}
