﻿using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using FahasaStoreAPI.Models.DTO;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : BaseController<Platform, PlatformModel, PlatformDTO, int>
    {
        public PlatformsController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Platform entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Platform> IncludeRelatedEntities(IQueryable<Platform> query)
        {
            return query;
        }
    }
}
