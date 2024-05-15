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
    public class WebsitesController : ControllerBase
    {
        private readonly FahasaStoreDBContext _context;

        public WebsitesController(FahasaStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Websites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Website>>> GetWebsites()
        {
          if (_context.Websites == null)
          {
              return NotFound();
          }
            return await _context.Websites.ToListAsync();
        }

        // GET: api/Websites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Website>> GetWebsite(int id)
        {
          if (_context.Websites == null)
          {
              return NotFound();
          }
            var website = await _context.Websites.FindAsync(id);

            if (website == null)
            {
                return NotFound();
            }

            return website;
        }

        // PUT: api/Websites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebsite(int id, Website website)
        {
            if (id != website.WebsiteId)
            {
                return BadRequest();
            }

            _context.Entry(website).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebsiteExists(id))
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

        // POST: api/Websites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Website>> PostWebsite(Website website)
        {
          if (_context.Websites == null)
          {
              return Problem("Entity set 'FahasaStoreDBContext.Websites'  is null.");
          }
            _context.Websites.Add(website);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWebsite", new { id = website.WebsiteId }, website);
        }

        // DELETE: api/Websites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebsite(int id)
        {
            if (_context.Websites == null)
            {
                return NotFound();
            }
            var website = await _context.Websites.FindAsync(id);
            if (website == null)
            {
                return NotFound();
            }

            _context.Websites.Remove(website);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebsiteExists(int id)
        {
            return (_context.Websites?.Any(e => e.WebsiteId == id)).GetValueOrDefault();
        }
    }
}
