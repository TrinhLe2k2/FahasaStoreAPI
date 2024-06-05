using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashSaleBooksController : BaseController<FlashSaleBook, FlashSaleModel, int>
    {
        public FlashSaleBooksController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(FlashSaleBook entity)
        {
            return entity.Id;
        }

        protected override IQueryable<FlashSaleBook> IncludeRelatedEntities(IQueryable<FlashSaleBook> query)
        {
            return query;
        }
    }
}
