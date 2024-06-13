using Microsoft.AspNetCore.Mvc;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models;
using FahasaStoreAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Models.Response;
using X.PagedList;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController : BaseController<Voucher, VoucherModel, VoucherDTO, int>
    {
        public VouchersController(FahasaStoreDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override int GetEntityId(Voucher entity)
        {
            return entity.Id;
        }

        protected override IQueryable<Voucher> IncludeRelatedEntities(IQueryable<Voucher> query)
        {
            return query.Include(e => e.Orders);
        }

        // API to get vouchers effective today with pagination and UsageLimit check
        [HttpGet("today")]
        public async Task<ActionResult<PaginatedResponse<VoucherDTO>>> GetVouchersToday(int page = 1, int size = 50)
        {
            var today = DateTime.Today;

            var vouchersQuery = _context.Vouchers
                .Include(v => v.Orders)
                .Where(v => v.StartDate <= today && v.EndDate >= today && v.Orders.Count() <= v.UsageLimit);

            var pagedVoucher = await vouchersQuery.ToPagedListAsync(page, size);

            var voucherDTOs = _mapper.Map<List<VoucherDTO>>(pagedVoucher);

            var response = new PaginatedResponse<VoucherDTO>
            {
                Items = voucherDTOs,
                PageNumber = pagedVoucher.PageNumber,
                PageSize = pagedVoucher.PageSize,
                TotalItemCount = pagedVoucher.TotalItemCount,
                PageCount = pagedVoucher.PageCount,
                HasNextPage = pagedVoucher.HasNextPage,
                HasPreviousPage = pagedVoucher.HasPreviousPage,
                IsFirstPage = pagedVoucher.IsFirstPage,
                IsLastPage = pagedVoucher.IsLastPage,
                StartPage = CalculateStartPage(pagedVoucher.PageNumber, pagedVoucher.PageCount, maxPages: 5),
                EndPage = CalculateEndPage(pagedVoucher.PageNumber, pagedVoucher.PageCount, maxPages: 5)
            };

            return Ok(response);
        }
    }
}
