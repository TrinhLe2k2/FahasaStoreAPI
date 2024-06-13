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
    public class TopicContentsController : BaseController<TopicContent, TopicContentModel, TopicContentDTO, int>
    {
        public TopicContentsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(TopicContent entity)
        {
            return entity.Id;
        }

        protected override IQueryable<TopicContent> IncludeRelatedEntities(IQueryable<TopicContent> query)
        {
            return query.Include(e => e.Topic);
        }
    }
}
