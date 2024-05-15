using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Entities;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverTypesController : ControllerBase
    {
        private readonly FahasaStoreDBContext _context;

        public CoverTypesController(FahasaStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/CoverTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoverType>>> GetCoverTypes()
        {
          if (_context.CoverTypes == null)
          {
              return NotFound();
          }
            return await _context.CoverTypes.ToListAsync();
        }

        // GET: api/CoverTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoverType>> GetCoverType(int id)
        {
          if (_context.CoverTypes == null)
          {
              return NotFound();
          }
            var coverType = await _context.CoverTypes.FindAsync(id);

            if (coverType == null)
            {
                return NotFound();
            }

            return coverType;
        }

        // PUT: api/CoverTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoverType(int id, CoverType coverType)
        {
            if (id != coverType.CoverTypeId)
            {
                return BadRequest();
            }

            _context.Entry(coverType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoverTypeExists(id))
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

        // POST: api/CoverTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CoverType>> PostCoverType(CoverType coverType)
        {
          if (_context.CoverTypes == null)
          {
              return Problem("Entity set 'FahasaStoreDBContext.CoverTypes'  is null.");
          }
            _context.CoverTypes.Add(coverType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoverType", new { id = coverType.CoverTypeId }, coverType);
        }

        // DELETE: api/CoverTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoverType(int id)
        {
            if (_context.CoverTypes == null)
            {
                return NotFound();
            }
            var coverType = await _context.CoverTypes.FindAsync(id);
            if (coverType == null)
            {
                return NotFound();
            }

            _context.CoverTypes.Remove(coverType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoverTypeExists(int id)
        {
            return (_context.CoverTypes?.Any(e => e.CoverTypeId == id)).GetValueOrDefault();
        }
    }
}
