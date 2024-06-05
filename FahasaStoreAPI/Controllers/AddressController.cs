using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseController<Address, AddressModel, int>
    {
        public AddressController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Address entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Address> IncludeRelatedEntities(IQueryable<Address> query)
        {
            return query;
        }
    }
}
