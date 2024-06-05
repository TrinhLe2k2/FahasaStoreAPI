﻿using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsitesController : BaseController<Website, WebsiteModel, int>
    {
        public WebsitesController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Website entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Website> IncludeRelatedEntities(IQueryable<Website> query)
        {
            return query;
        }
    }
}
