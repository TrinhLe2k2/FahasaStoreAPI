using AutoMapper;
using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Helpers;
using FahasaStoreAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly FahasaStoreDBContext _context;
        private readonly IMapper _mapper;

        public StatisticsController(FahasaStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("GetYearlyStatistics")]
        public async Task<ActionResult<IEnumerable<MonthlyStatisticsDTO>>> GetYearlyStatistics(int? year)
        {
            var statistics = new List<MonthlyStatisticsDTO>();
            int targetYear = year ?? DateTime.Now.Year;
            int currentMonth = targetYear == DateTime.Now.Year ? DateTime.Now.Month : 12;

            for (int month = 1; month <= currentMonth; month++)
            {
                var startDate = new DateTime(targetYear, month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var ordersInMonth = await _context.Orders
                    .Include(e => e.Address)
                    .Include(e => e.PaymentMethod)
                    .Include(e => e.User)
                    .Include(e => e.Voucher)
                    .Include(e => e.Payment)
                    .Include(e => e.Reviews)
                    .Include(e => e.OrderItems)
                        .ThenInclude(oi => oi.Book)
                            .ThenInclude(b => b.PosterImages)
                    .Include(e => e.OrderStatuses)
                        .ThenInclude(o => o.Status)
                    .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate)
                    .ToListAsync();

                var deliveredOrders = ordersInMonth.Where(o => o.OrderStatuses.Any(o => o.Status.Name == AppStatus.Completed)).ToList();
                var cancelledOrders = ordersInMonth.Where(o => o.OrderStatuses.Any(o => o.Status.Name == AppStatus.Cancelled)).ToList();

                var newUsersInMonth = await _context.AspNetUsers
                    .Where(u => u.CreatedAt >= startDate && u.CreatedAt <= endDate)
                    .CountAsync();

                var top10Books = ordersInMonth
                    .SelectMany(o => o.OrderItems)
                    .GroupBy(oi => oi.Book)
                    .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                    .Take(10)
                    .Select(g => 
                    {
                        var bookDto = _mapper.Map<BookDTO>(g.Key);
                        bookDto.QuantitySold = g.Sum(oi => oi.Quantity);
                        return bookDto;
                    })
                    .ToList();


                var totalBooksSold = ordersInMonth.SelectMany(o => o.OrderItems).Sum(oi => oi.Quantity);
                var totalRevenue = ordersInMonth.SelectMany(o => o.OrderItems).Where(o => o.Order.OrderStatuses.Any(o => o.Status.Name == AppStatus.Completed)).Sum(oi => oi.Quantity * oi.Book.Price); // cần chỉnh sửa OrderItems

                var monthlyStatistics = new MonthlyStatisticsDTO
                {
                    Month = month,
                    TotalRevenue = totalRevenue,
                    TotalOrdersCompleted = deliveredOrders.Count,
                    TotalOrdersCancelled = cancelledOrders.Count,
                    Top10Books = top10Books,
                    NewUsers = newUsersInMonth,
                    TotalBooksSold = totalBooksSold
                };

                statistics.Add(monthlyStatistics);
            }

            return Ok(statistics);
        }

    }
}
