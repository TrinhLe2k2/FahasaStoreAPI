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
    public class SocialMediaLinksController : ControllerBase
    {
        private readonly FahasaStoreDBContext _context;

        public SocialMediaLinksController(FahasaStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/SocialMediaLinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocialMediaLink>>> GetSocialMediaLinks()
        {
          if (_context.SocialMediaLinks == null)
          {
              return NotFound();
          }
            return await _context.SocialMediaLinks.ToListAsync();
        }

        // GET: api/SocialMediaLinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SocialMediaLink>> GetSocialMediaLink(int id)
        {
          if (_context.SocialMediaLinks == null)
          {
              return NotFound();
          }
            var socialMediaLink = await _context.SocialMediaLinks.FindAsync(id);

            if (socialMediaLink == null)
            {
                return NotFound();
            }

            return socialMediaLink;
        }

        // PUT: api/SocialMediaLinks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocialMediaLink(int id, SocialMediaLink socialMediaLink)
        {
            if (id != socialMediaLink.LinkId)
            {
                return BadRequest();
            }

            _context.Entry(socialMediaLink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialMediaLinkExists(id))
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

        // POST: api/SocialMediaLinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SocialMediaLink>> PostSocialMediaLink(SocialMediaLink socialMediaLink)
        {
          if (_context.SocialMediaLinks == null)
          {
              return Problem("Entity set 'FahasaStoreDBContext.SocialMediaLinks'  is null.");
          }
            _context.SocialMediaLinks.Add(socialMediaLink);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSocialMediaLink", new { id = socialMediaLink.LinkId }, socialMediaLink);
        }

        // DELETE: api/SocialMediaLinks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocialMediaLink(int id)
        {
            if (_context.SocialMediaLinks == null)
            {
                return NotFound();
            }
            var socialMediaLink = await _context.SocialMediaLinks.FindAsync(id);
            if (socialMediaLink == null)
            {
                return NotFound();
            }

            _context.SocialMediaLinks.Remove(socialMediaLink);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SocialMediaLinkExists(int id)
        {
            return (_context.SocialMediaLinks?.Any(e => e.LinkId == id)).GetValueOrDefault();
        }
    }
}
