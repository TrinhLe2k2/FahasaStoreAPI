﻿using System;
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
    public class BooksController : ControllerBase
    {
        private readonly FahasaStoreDBContext _context;

        public BooksController(FahasaStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            return await _context.Books
                .Include(e => e.Author)
                  .Include(e => e.CoverType)
                  .Include(e => e.Dimension)
                  .Include(e => e.Subcategory)
                  .Include(e => e.CartItems)
                  .Include(e => e.FlashSaleBooks)
                  .Include(e => e.OrderItems)
                  .Include(e => e.PosterImages)
                  .Include(e => e.Reviews)
                  .Include(e => e.BookPartners)
                  .ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            var book = await _context.Books
                .Include(e => e.Author)
                  .Include(e => e.CoverType)
                  .Include(e => e.Dimension)
                  .Include(e => e.Subcategory)
                  .Include(e => e.CartItems)
                  .Include(e => e.FlashSaleBooks)
                  .Include(e => e.OrderItems)
                  .Include(e => e.PosterImages)
                  .Include(e => e.Reviews)
                  .Include(e => e.BookPartners)
                  .FirstOrDefaultAsync(e => e.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
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
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (_context.Books == null)
          {
              return Problem("Entity set 'FahasaStoreDBContext.Books'  is null.");
          }
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

        [HttpGet("GetPosterImages/{id}")]
        public async Task<ActionResult<IEnumerable<PosterImage>>> GetPosterImages(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books
                  .Include(e => e.PosterImages)
                  .FirstOrDefaultAsync(e => e.BookId == id);

            if (book == null)
            {
                return NotFound();
            }
            var posterImages = book.PosterImages.ToList();
            return posterImages;
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
