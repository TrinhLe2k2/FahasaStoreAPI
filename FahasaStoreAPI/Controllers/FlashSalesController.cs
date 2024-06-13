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
    public class FlashSalesController : BaseController<FlashSale, FlashSaleModel, FlashSaleDTO, int>
    {
        public FlashSalesController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(FlashSale entity)
        {
            return entity.Id;
        }

        protected override IQueryable<FlashSale> IncludeRelatedEntities(IQueryable<FlashSale> query)
        {
            return query.Include(e => e.FlashSaleBooks);
        }
    }
}
