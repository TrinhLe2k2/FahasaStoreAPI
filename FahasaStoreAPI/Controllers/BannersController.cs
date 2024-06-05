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
    public class BannersController : BaseController<Banner, BannerModel, int>
    {
        public BannersController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Banner entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Banner> IncludeRelatedEntities(IQueryable<Banner> query)
        {
            return query;
        }
    }
}
