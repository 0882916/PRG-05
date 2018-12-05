using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCGallery.Models;

namespace MVCGallery.Controllers
{
    public class PicturesController : Controller
    {
        private readonly MVCGalleryContext _context;

        public PicturesController(MVCGalleryContext context)
        {
            _context = context;
        }

        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public async Task<IActionResult> Index(string pictureGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from p in _context.Picture
                                            orderby p.Genre
                                            select p.Genre;

            var pictures = from p in _context.Picture
                         select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                pictures = pictures.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(pictureGenre))
            {
                pictures = pictures.Where(x => x.Genre == pictureGenre);
            }

            var pictureGenreVM = new PictureGenreViewModel();
            pictureGenreVM.Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            pictureGenreVM.Pictures = await pictures.ToListAsync();
            pictureGenreVM.SearchString = searchString;

            return View(pictureGenreVM);
        }

        // GET: Pictures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Picture
                .FirstOrDefaultAsync(p => p.ID == id);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // GET: Pictures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Picture picture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(picture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(picture);
        }

        // GET: Pictures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Picture.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }
            return View(picture);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Picture picture)
        {
            if (id != picture.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(picture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PictureExists(picture.ID))
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
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Picture
                .FirstOrDefaultAsync(m => m.ID == id);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var picture = await _context.Picture.FindAsync(id);
            _context.Picture.Remove(picture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PictureExists(int id)
        {
            return _context.Picture.Any(e => e.ID == id);
        }
    }
}
