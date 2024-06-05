using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : BaseController<Notification, NotificationModel, int>
    {
        public NotificationsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Notification entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Notification> IncludeRelatedEntities(IQueryable<Notification> query)
        {
            return query;
        }
    }
}
