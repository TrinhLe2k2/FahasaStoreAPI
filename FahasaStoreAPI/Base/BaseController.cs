using AutoMapper;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Entities;
using X.PagedList;
using System.Reflection;
using FahasaStoreAPI.Models.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing.Printing;

namespace FahasaStoreAPI.Controllers
{
    public abstract class BaseController<TEntity, TModel, DTO, TKey> : ControllerBase
        where TEntity : class
        where TModel : class
        where DTO : class
        where TKey : IEquatable<TKey>
    {
        protected readonly FahasaStoreDBContext _context;
        protected readonly IMapper _mapper;
        private readonly List<Expression<Func<TEntity, object>>> _includes;

        protected BaseController(FahasaStoreDBContext context, IMapper mapper, params Expression<Func<TEntity, object>>[] includes)
        {
            _context = context;
            _mapper = mapper;
            _includes = includes.ToList();
        }

        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<DTO>>> GetEntities()
        {
            if (_context.Set<TEntity>() == null)
            {
                return Problem($"Entity set '{typeof(TEntity).Name}' is null.");
            }

            var entities = await IncludeRelatedEntities(_context.Set<TEntity>())
                                .OrderByDescending(e => EF.Property<object>(e, "CreatedAt"))
                                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<DTO>>(entities));
        }
        // GET: api/[controller]/Pagination
        [HttpGet("Pagination")]
        public virtual async Task<ActionResult<PaginatedResponse<DTO>>> GetPagination(int page = 1, int size = 10)
        {
            if (_context.Set<TEntity>() == null)
            {
                return Problem($"Entity set '{typeof(TEntity).Name}' is null.");
            }

            int pageNumber = page;
            int pageSize = size;

            var query = IncludeRelatedEntities(_context.Set<TEntity>())
                        .OrderByDescending(e => EF.Property<object>(e, "CreatedAt"));

            var pagedList = await query.ToPagedListAsync(pageNumber, pageSize);

            // Map entities to DTOs
            var dtoList = pagedList.Select(entity => _mapper.Map<DTO>(entity)).ToList();

            // Prepare paginated response
            var response = new PaginatedResponse<DTO>
            {
                Items = dtoList,
                PageNumber = pagedList.PageNumber,
                PageSize = pagedList.PageSize,
                TotalItemCount = pagedList.TotalItemCount,
                PageCount = pagedList.PageCount,
                HasNextPage = pagedList.HasNextPage,
                HasPreviousPage = pagedList.HasPreviousPage,
                IsFirstPage = pagedList.IsFirstPage,
                IsLastPage = pagedList.IsLastPage,
                StartPage = CalculateStartPage(pagedList.PageNumber, pagedList.PageCount, maxPages: 5),
                EndPage = CalculateEndPage(pagedList.PageNumber, pagedList.PageCount, maxPages: 5)
            };

            return Ok(response);
        }
        // GET: api/[controller]/{id}
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<DTO>> GetEntity(TKey id)
        {
            if (_context.Set<TEntity>() == null)
            {
                return Problem($"Entity set '{typeof(TEntity).Name}' is null.");
            }

            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var entityWithIncludes = IncludeRelatedEntities(_context.Set<TEntity>())
                .AsEnumerable()
                .FirstOrDefault(e => GetEntityId(e).Equals(id));

            return Ok(_mapper.Map<DTO>(entityWithIncludes));
        }
        // PUT: api/[controller]/{id}
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutEntity(TModel model, TKey id)
        {
            if (_context.Set<TEntity>() == null)
            {
                return Problem($"Entity set '{typeof(TEntity).Name}' is null.");
            }
            
            var entity = _mapper.Map<TEntity>(model);

            if (!GetEntityId(entity).Equals(id))
            {
                return BadRequest();
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPost]
        public virtual async Task<ActionResult<TModel>> PostEntity(TModel model)
        {
            if (_context.Set<TEntity>() == null)
            {
                return Problem($"Entity set '{typeof(TEntity).Name}' is null.");
            }

            var entity = _mapper.Map<TEntity>(model);

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            model = _mapper.Map<TModel>(entity);
            return CreatedAtAction(nameof(GetEntity), new { id = GetEntityId(entity) }, model);
        }
        // DELETE: api/[controller]/{id}
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteEntity(TKey id)
        {
            if (_context.Set<TEntity>() == null)
            {
                return NotFound();
            }
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected bool EntityExists(TKey id)
        {
            return (_context.Set<TEntity>()?.Find(id) != null);
        }
        protected abstract TKey GetEntityId(TEntity entity);
        protected virtual IQueryable<TEntity> IncludeRelatedEntities(IQueryable<TEntity> query)
        {
            foreach (var include in _includes)
            {
                query = query.Include(include);
            }
            return query;
        }
        // GET: api/[controller]/FilterPagination
        [HttpGet("FilterPagination")]
        public virtual async Task<ActionResult<PaginatedResponse<DTO>>> GetFilteredPagination(
            [FromQuery] Dictionary<string, string>? filters,
            string? sortField,
            string? sortDirection,
            int page = 1,
            int size = 10)
        {
            sortField ??= "CreatedAt";
            sortDirection ??= "desc";

            if (_context.Set<TEntity>() == null)
            {
                return Problem($"Entity set '{typeof(TEntity).Name}' is null.");
            }

            int pageNumber = page;
            int pageSize = size;

            // Khởi tạo query
            var query = IncludeRelatedEntities(_context.Set<TEntity>());

            // Áp dụng các bộ lọc từ filters
            if (filters != null && filters.Count > 0)
            {
                foreach (var filter in filters)
                {
                    var property = typeof(TEntity).GetProperty(filter.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property != null)
                    {
                        var value = Convert.ChangeType(filter.Value, property.PropertyType);
                        query = query.Where(e => EF.Property<object>(e, property.Name).Equals(value));
                    }
                }
            }

            // Sắp xếp theo sortField và sortDirection
            query = ApplySorting(query, sortField, sortDirection);

            // Phân trang
            var pagedList = await query.ToPagedListAsync(pageNumber, pageSize);

            // Map entities to DTOs
            var dtoList = pagedList.Select(entity => _mapper.Map<DTO>(entity)).ToList();

            // Chuẩn bị phản hồi phân trang
            var response = new PaginatedResponse<DTO>
            {
                Items = dtoList,
                PageNumber = pagedList.PageNumber,
                PageSize = pagedList.PageSize,
                TotalItemCount = pagedList.TotalItemCount,
                PageCount = pagedList.PageCount,
                HasNextPage = pagedList.HasNextPage,
                HasPreviousPage = pagedList.HasPreviousPage,
                IsFirstPage = pagedList.IsFirstPage,
                IsLastPage = pagedList.IsLastPage,
                StartPage = CalculateStartPage(pagedList.PageNumber, pagedList.PageCount, maxPages: 5),
                EndPage = CalculateEndPage(pagedList.PageNumber, pagedList.PageCount, maxPages: 5)
            };

            return Ok(response);
        }
        protected IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, string? sortField, string? sortDirection)
        {
            sortField ??= "CreatedAt";
            sortDirection ??= "desc";

            var property = typeof(TEntity).GetProperty(sortField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
            {
                // Nếu sortField không tồn tại, mặc định sắp xếp theo CreatedAt
                property = typeof(TEntity).GetProperty("CreatedAt");
            }
            if (property == null)
            {
                return query;
            }
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            string methodName = sortDirection.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";
            var resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TEntity), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));

            return query.Provider.CreateQuery<TEntity>(resultExp);
        }
        protected int CalculateStartPage(int currentPage, int totalPages, int maxPages)
        {
            int startPage = Math.Max(1, currentPage - maxPages / 2);
            if (startPage + maxPages - 1 > totalPages)
            {
                startPage = Math.Max(1, totalPages - maxPages + 1);
            }
            return startPage;
        }
        protected int CalculateEndPage(int currentPage, int totalPages, int maxPages)
        {
            int endPage = Math.Min(totalPages, currentPage + maxPages / 2);
            if (endPage < maxPages)
            {
                endPage = Math.Min(totalPages, maxPages);
            }
            return endPage;
        }

        [HttpGet("GetListBy/{propertyName}/{value}")]
        public virtual async Task<ActionResult<PaginatedResponse<DTO>>> GetListBy(string propertyName, string value, int page = 1, int size = 10)
        {
            try
            {
                if (_context.Set<TEntity>() == null)
                {
                    return Problem($"Entity set '{typeof(TEntity).Name}' is null.");
                }

                if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(value))
                {
                    return BadRequest("PropertyName and value must not be empty.");
                }

                // Nếu value là "0", trả về tất cả các thực thể
                if (value == "0")
                {
                    var getAll = IncludeRelatedEntities(_context.Set<TEntity>())
                        .OrderByDescending(e => EF.Property<object>(e, "CreatedAt"));

                    return await CreatePaginatedResponse(getAll, page, size);
                }

                // Kiểm tra xem propertyName có tồn tại trong TEntity không
                var entityType = typeof(TEntity);
                var propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    return BadRequest($"Property '{propertyName}' does not exist in '{entityType.Name}'.");
                }

                // Tiếp tục xử lý nếu propertyName tồn tại
                var entities = IncludeRelatedEntities(_context.Set<TEntity>())
                    .Where(GetFilterExpression(propertyName, value))
                    .OrderByDescending(e => EF.Property<object>(e, "CreatedAt"));

                return await CreatePaginatedResponse(entities, page, size);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private Expression<Func<TEntity, bool>> GetFilterExpression(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(Convert.ChangeType(value, property.Type));
            var equal = Expression.Equal(property, constant);

            return Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
        }

        private async Task<ActionResult<PaginatedResponse<DTO>>> CreatePaginatedResponse(IQueryable<TEntity> query, int page, int size)
        {
            var pagedList = await query.ToPagedListAsync(page, size);
            var dtoList = _mapper.Map<IEnumerable<DTO>>(pagedList).ToList();

            return Ok(new PaginatedResponse<DTO>
            {
                Items = dtoList,
                PageNumber = pagedList.PageNumber,
                PageSize = pagedList.PageSize,
                TotalItemCount = pagedList.TotalItemCount,
                PageCount = pagedList.PageCount,
                HasNextPage = pagedList.HasNextPage,
                HasPreviousPage = pagedList.HasPreviousPage,
                IsFirstPage = pagedList.IsFirstPage,
                IsLastPage = pagedList.IsLastPage,
                StartPage = CalculateStartPage(pagedList.PageNumber, pagedList.PageCount, maxPages: 5),
                EndPage = CalculateEndPage(pagedList.PageNumber, pagedList.PageCount, maxPages: 5)
            });
        }

    }
}
