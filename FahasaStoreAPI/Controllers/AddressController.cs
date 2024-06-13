using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using FahasaStoreAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseController<Address, AddressModel, AddressDTO, int>
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
            return query.Include(e => e.User).Include(e => e.Orders);
        }

        [HttpPost]
        public override async Task<ActionResult<AddressModel>> PostEntity(AddressModel model)
        {
            var existingDefaultAddress = _context.Set<Address>().FirstOrDefault(a => a.DefaultAddress && a.UserId == model.UserId);
            if (existingDefaultAddress == null)
            {
                model.DefaultAddress = true;
            }
            else if (model.DefaultAddress)
            {
                var allAddressesUser = _context.Set<Address>().Where(a => a.DefaultAddress && a.UserId == model.UserId);
                foreach (var address in allAddressesUser)
                {
                    address.DefaultAddress = false;
                }
            }
            return await base.PostEntity(model);
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> PutEntity(AddressModel model, int id)
        {
            var existingDefaultAddress = _context.Set<Address>().FirstOrDefault(a => a.DefaultAddress && a.Id != id && a.UserId == model.UserId);
            if (existingDefaultAddress == null)
            {
                model.DefaultAddress = true;
            }
            else if (model.DefaultAddress)
            {
                var allAddresses = _context.Set<Address>().Where(a => a.DefaultAddress && a.Id != id && a.UserId == model.UserId);
                foreach (var address in allAddresses)
                {
                    address.DefaultAddress = false;
                    _context.Entry(address).State = EntityState.Modified; // Đánh dấu các địa chỉ khác là đã sửa đổi
                }
            }
            return await base.PutEntity(model, id);
        }
        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteEntity(int id)
        {
            var AddressDelete = _context.Set<Address>().FirstOrDefault(a => a.Id == id);
            if(AddressDelete == null)
            {
                return NotFound();
            } 
            var existingDefaultAddress = _context.Set<Address>().FirstOrDefault(a => a.DefaultAddress && a.Id != id && a.UserId == AddressDelete.UserId);
            if (existingDefaultAddress == null)
            {
                var newDefaultAddress = await _context.Set<Address>().Where(a => a.Id != id && a.UserId == AddressDelete.UserId).FirstOrDefaultAsync();
                if (newDefaultAddress != null)
                {
                    newDefaultAddress.DefaultAddress = true;
                    _context.Entry(newDefaultAddress).State = EntityState.Modified;
                }
            }
            return await base.DeleteEntity(id);
        }
    }
}
