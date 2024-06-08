using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<AspNetUser, AspNetUserModel, string>
    {
        public UsersController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override string GetEntityId(AspNetUser entity)
        {
            return entity.Id;
        }

        protected override IQueryable<AspNetUser> IncludeRelatedEntities(IQueryable<AspNetUser> query)
        {
            return query
                .Include(e => e.Cart)
                    .ThenInclude(e => e.CartItems)
                .Include(e => e.Addresses)
                .Include(e => e.Favourites)
                .Include(e => e.Notifications)
                .Include(e => e.Orders)
                .Include(e => e.Reviews)
                .Include(e => e.Roles);
        }
    }
}
