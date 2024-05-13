using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Models.FormModels;
using AutoMapper;
using FahasaStoreAPI.Models.EntitiesModels;
using FahasaStoreAPI.Models.BasicModels;

namespace FahasaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly FahasaStoreDBContext _context;
        private readonly IMapper _mapper;

        public BooksController(FahasaStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookEntities>>> GetBooks()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var books = await _context.Books
                  .Include(e => e.Author)
                  .Include(e => e.CoverType)
                  .Include(e => e.Dimension)
                  .Include(e => e.Partner)
                  .Include(e => e.Subcategory)
                  .Include(e => e.CartItems)
                  .Include(e => e.FlashSaleBooks)
                  .Include(e => e.OrderItems)
                  .Include(e => e.PosterImages)
                  .Include(e => e.Reviews)
                  .ToListAsync();
            var booksEntities = _mapper.Map<List<BookEntities>>(books);
            return booksEntities;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookEntities>> GetBook(int id)
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            var book = await _context.Books
                .Include(e => e.Author)
                  .Include(e => e.CoverType)
                  .Include(e => e.Dimension)
                  .Include(e => e.Partner)
                  .Include(e => e.Subcategory)
                  .Include(e => e.CartItems)
                  .Include(e => e.FlashSaleBooks)
                  .Include(e => e.OrderItems)
                  .Include(e => e.PosterImages)
                  .Include(e => e.Reviews)
                .FirstOrDefaultAsync(e => e.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return _mapper.Map<BookEntities>(book);
        }

        [HttpGet("PutBook/{id}")]
        public async Task<ActionResult<BookForm>> PutBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FirstOrDefaultAsync(e => e.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return _mapper.Map<BookForm>(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookForm bookForm)
        {
            var book = _mapper.Map<Book>(bookForm);
            if (id != book.BookId)
            {
                return BadRequest();
            }
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookForm bookForm)
        {
          if (_context.Books == null)
          {
              return Problem("Entity set 'FahasaStoreDBContext.Books'  is null.");
          }
          var book = _mapper.Map<Book>(bookForm);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
