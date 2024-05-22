using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Entities;
using AutoMapper;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosterImagesController : ControllerBase
    {
        private readonly FahasaStoreDBContext _context;
        private readonly IMapper _mapper;

        public PosterImagesController(FahasaStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/PosterImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosterImage>>> GetPosterImages()
        {
          if (_context.PosterImages == null)
          {
              return NotFound();
          }
            return await _context.PosterImages.ToListAsync();
        }

        // GET: api/PosterImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosterImage>> GetPosterImage(int id)
        {
          if (_context.PosterImages == null)
          {
              return NotFound();
          }
            var posterImage = await _context.PosterImages.FindAsync(id);

            if (posterImage == null)
            {
                return NotFound();
            }

            return posterImage;
        }

        // PUT: api/PosterImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosterImage(int id, PosterImage posterImage)
        {
            if (id != posterImage.PosterImageId)
            {
                return BadRequest();
            }

            _context.Entry(posterImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosterImageExists(id))
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

        // POST: api/PosterImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PosterImage>> PostPosterImage(PosterImage posterImage)
        {
          if (_context.PosterImages == null)
          {
              return Problem("Entity set 'FahasaStoreDBContext.PosterImages'  is null.");
          }
            _context.PosterImages.Add(posterImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosterImage", new { id = posterImage.PosterImageId }, posterImage);
        }

        // DELETE: api/PosterImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosterImage(int id)
        {
            if (_context.PosterImages == null)
            {
                return NotFound();
            }
            var posterImage = await _context.PosterImages.FindAsync(id);
            if (posterImage == null)
            {
                return NotFound();
            }

            _context.PosterImages.Remove(posterImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PosterImageExists(int id)
        {
            return (_context.PosterImages?.Any(e => e.PosterImageId == id)).GetValueOrDefault();
        }
    }
}
