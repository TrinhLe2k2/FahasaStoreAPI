using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Entities;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using FahasaStoreAPI.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;

namespace FahasaStoreAPI.Controllers
{
    public abstract class BaseController<TEntity, TModel, TKey> : ControllerBase
        where TEntity : class
        where TModel : class
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
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetEntities()
        {
            if (_context.Set<TEntity>() == null)
            {
                return Problem($"Entity set '{typeof(TEntity).Name}' is null.");
            }

            var entities = await IncludeRelatedEntities(_context.Set<TEntity>())
                                .OrderByDescending(e => EF.Property<object>(e, "CreatedAt"))
                                .ToListAsync();

            return Ok(entities);
        }
        // GET: api/[controller]/Pagination
        [HttpGet("Pagination")]
        public virtual async Task<ActionResult<IPagedList<TEntity>>> GetPagination(int page = 1, int size = 10)
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

            return Ok(new StaticPagedList<TEntity>(pagedList, pagedList.GetMetaData()));
        }
        // GET: api/[controller]/{id}
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetEntity(TKey id)
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

            return Ok(entityWithIncludes);
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
        // Phương thức GetListBy
        [HttpGet("GetListBy/{propertyName}/{value}")]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetListBy(string propertyName, string value, int limited = 10)
        {
            if (_context.Set<TEntity>() == null)
            {
                return Problem($"Entity set '{typeof(TEntity).Name}' is null.");
            }

            // Kiểm tra nếu propertyName hoặc value là rỗng
            if (value == "0")
            {
                var getAll = await IncludeRelatedEntities(_context.Set<TEntity>())
                                        .OrderByDescending(e => EF.Property<object>(e, "CreatedAt"))
                                        .Take(limited)
                                        .ToListAsync(); // Truy vấn toàn bộ TEntity
                return Ok(getAll);
            }

            // Kiểm tra xem propertyName có tồn tại trong TEntity không
            var entityType = typeof(TEntity);
            var propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                return BadRequest($"Property '{propertyName}' does not exist in '{entityType.Name}'.");
            }

            // Tiếp tục xử lý nếu propertyName tồn tại
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Constant(Convert.ChangeType(value, property.Type));
            var equal = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);

            var entities = await IncludeRelatedEntities(_context.Set<TEntity>())
                                .Where(lambda)
                                .OrderByDescending(e => EF.Property<object>(e, "CreatedAt"))
                                .ToListAsync();

            return Ok(entities);
        }

        private bool EntityExists(TKey id)
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
    }
}
