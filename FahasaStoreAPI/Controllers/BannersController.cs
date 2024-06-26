﻿using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using FahasaStoreAPI.Models.DTO;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : BaseController<Banner, BannerModel, BannerDTO, int>
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
