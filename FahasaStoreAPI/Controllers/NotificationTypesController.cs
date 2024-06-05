using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationTypesController : BaseController<NotificationType, NotificationTypeModel, int>
    {
        public NotificationTypesController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(NotificationType entity)
        {
            return entity.Id;
        }

        protected override IQueryable<NotificationType> IncludeRelatedEntities(IQueryable<NotificationType> query)
        {
            return query;
        }
    }
}
