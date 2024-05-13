using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FahasaStoreAPI.Entities;

namespace FahasaStoreAPI.Controllers
{
    public class Books1Controller : Controller
    {
        private readonly FahasaStoreDBContext _context;

        public Books1Controller(FahasaStoreDBContext context)
        {
            _context = context;
        }

        // GET: Books1
        public async Task<IActionResult> Index()
        {
            var fahasaStoreDBContext = _context.Books.Include(b => b.Author).Include(b => b.CoverType).Include(b => b.Dimension).Include(b => b.Partner).Include(b => b.Subcategory);
            return View(await fahasaStoreDBContext.ToListAsync());
        }

        // GET: Books1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.CoverType)
                .Include(b => b.Dimension)
                .Include(b => b.Partner)
                .Include(b => b.Subcategory)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books1/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId");
            ViewData["CoverTypeId"] = new SelectList(_context.CoverTypes, "CoverTypeId", "CoverTypeId");
            ViewData["DimensionId"] = new SelectList(_context.Dimensions, "DimensionId", "DimensionId");
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "PartnerId");
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryId");
            return View();
        }

        // POST: Books1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,SubcategoryId,PartnerId,AuthorId,CoverTypeId,DimensionId,Name,Description,OriginalPrice,CurrentPrice,DiscountPercentage,Quantity,Weight,PageCount")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", book.AuthorId);
            ViewData["CoverTypeId"] = new SelectList(_context.CoverTypes, "CoverTypeId", "CoverTypeId", book.CoverTypeId);
            ViewData["DimensionId"] = new SelectList(_context.Dimensions, "DimensionId", "DimensionId", book.DimensionId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "PartnerId", book.PartnerId);
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryId", book.SubcategoryId);
            return View(book);
        }

        // GET: Books1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", book.AuthorId);
            ViewData["CoverTypeId"] = new SelectList(_context.CoverTypes, "CoverTypeId", "CoverTypeId", book.CoverTypeId);
            ViewData["DimensionId"] = new SelectList(_context.Dimensions, "DimensionId", "DimensionId", book.DimensionId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "PartnerId", book.PartnerId);
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryId", book.SubcategoryId);
            return View(book);
        }

        // POST: Books1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,SubcategoryId,PartnerId,AuthorId,CoverTypeId,DimensionId,Name,Description,OriginalPrice,CurrentPrice,DiscountPercentage,Quantity,Weight,PageCount")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", book.AuthorId);
            ViewData["CoverTypeId"] = new SelectList(_context.CoverTypes, "CoverTypeId", "CoverTypeId", book.CoverTypeId);
            ViewData["DimensionId"] = new SelectList(_context.Dimensions, "DimensionId", "DimensionId", book.DimensionId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "PartnerId", book.PartnerId);
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryId", book.SubcategoryId);
            return View(book);
        }

        // GET: Books1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.CoverType)
                .Include(b => b.Dimension)
                .Include(b => b.Partner)
                .Include(b => b.Subcategory)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'FahasaStoreDBContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
