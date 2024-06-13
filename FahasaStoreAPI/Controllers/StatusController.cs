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
    public class StatusController : BaseController<Status, StatusModel, StatusDTO, int>
    {
        public StatusController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Status entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Status> IncludeRelatedEntities(IQueryable<Status> query)
        {
            return query.Include(e => e.OrderStatuses);
        }
    }
}
