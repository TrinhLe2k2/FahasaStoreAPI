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
    public class PartnerTypesController : ControllerBase
    {
        private readonly FahasaStoreDBContext _context;

        public PartnerTypesController(FahasaStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/PartnerTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartnerType>>> GetPartnerTypes()
        {
          if (_context.PartnerTypes == null)
          {
              return NotFound();
          }
            return await _context.PartnerTypes.Include(e=>e.Partners).ToListAsync();
        }

        // GET: api/PartnerTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PartnerType>> GetPartnerType(int id)
        {
          if (_context.PartnerTypes == null)
          {
              return NotFound();
          }
            var partnerType = await _context.PartnerTypes.FirstOrDefaultAsync(e=>e.PartnerTypeId == id);

            if (partnerType == null)
            {
                return NotFound();
            }

            return partnerType;
        }

        // PUT: api/PartnerTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartnerType(int id, PartnerType partnerType)
        {
            if (id != partnerType.PartnerTypeId)
            {
                return BadRequest();
            }

            _context.Entry(partnerType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartnerTypeExists(id))
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

        // POST: api/PartnerTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PartnerType>> PostPartnerType(PartnerType partnerType)
        {
          if (_context.PartnerTypes == null)
          {
              return Problem("Entity set 'FahasaStoreDBContext.PartnerTypes'  is null.");
          }
            _context.PartnerTypes.Add(partnerType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartnerType", new { id = partnerType.PartnerTypeId }, partnerType);
        }

        // DELETE: api/PartnerTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartnerType(int id)
        {
            if (_context.PartnerTypes == null)
            {
                return NotFound();
            }
            var partnerType = await _context.PartnerTypes.FindAsync(id);
            if (partnerType == null)
            {
                return NotFound();
            }

            _context.PartnerTypes.Remove(partnerType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetPartnersByType/{id}")]
        public async Task<ActionResult<IEnumerable<Partner>>> GetPartnersByType(int id)
        {
            if (_context.PartnerTypes == null)
            {
                return NotFound();
            }
            var partnerType = await _context.PartnerTypes.Include(e => e.Partners).FirstOrDefaultAsync(e => e.PartnerTypeId == id);

            if (partnerType == null)
            {
                return NotFound();
            }
            var partners = partnerType.Partners.ToList();
            return partners;
        }

        private bool PartnerTypeExists(int id)
        {
            return (_context.PartnerTypes?.Any(e => e.PartnerTypeId == id)).GetValueOrDefault();
        }
    }
}
