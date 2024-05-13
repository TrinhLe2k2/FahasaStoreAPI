using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Entities;
using AutoMapper;
using FahasaStoreAPI.Models.FormModels;
using FahasaStoreAPI.Models.EntitiesModels;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DimensionsController : ControllerBase
    {
        private readonly FahasaStoreDBContext _context;
        private readonly IMapper _mapper;

        public DimensionsController(FahasaStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Dimensions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimensionEntities>>> GetDimensions()
        {
          if (_context.Dimensions == null)
          {
              return NotFound();
          }
          var dimensions = await _context.Dimensions.Include(e=>e.Books).ToListAsync();
            return _mapper.Map<List<DimensionEntities>>(dimensions);
        }

        // GET: api/Dimensions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimensionEntities>> GetDimension(int id)
        {
          if (_context.Dimensions == null)
          {
              return NotFound();
          }
            var dimension = await _context.Dimensions.Include(e => e.Books).FirstOrDefaultAsync(e=> e.DimensionId == id);

            if (dimension == null)
            {
                return NotFound();
            }

            return _mapper.Map<DimensionEntities>(dimension);
        }

        // PUT: api/Dimensions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimension(int id, Dimension dimension)
        {
            if (id != dimension.DimensionId)
            {
                return BadRequest();
            }

            _context.Entry(dimension).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimensionExists(id))
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

        // POST: api/Dimensions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dimension>> PostDimension(DimensionForm dimensionForm)
        {
          if (_context.Dimensions == null)
          {
              return Problem("Entity set 'FahasaStoreDBContext.Dimensions'  is null.");
          }
            var dimension = _mapper.Map<Dimension>(dimensionForm);
            _context.Dimensions.Add(dimension);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDimension", new { id = dimension.DimensionId }, dimension);
        }

        // DELETE: api/Dimensions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimension(int id)
        {
            if (_context.Dimensions == null)
            {
                return NotFound();
            }
            var dimension = await _context.Dimensions.FindAsync(id);
            if (dimension == null)
            {
                return NotFound();
            }

            _context.Dimensions.Remove(dimension);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimensionExists(int id)
        {
            return (_context.Dimensions?.Any(e => e.DimensionId == id)).GetValueOrDefault();
        }
    }
}
