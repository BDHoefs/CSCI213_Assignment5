using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment5.Models;

namespace Assignment5.Controllers
{
    public class SongsController : Controller
    {
        private readonly MusicStoreContext _context;

        public SongsController(MusicStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var musicStoreContext = _context.Songs.Include(s => s.Genre).Include(s => s.Musician);
            return View(await musicStoreContext.ToListAsync());
        }

        // GET: Songs
        public async Task<IActionResult> Browse(string musicianFilter, string genreFilter)
        {
            var songs = _context.Songs.Include(s => s.Musician).Include(s => s.Genre);

            if (musicianFilter != null)
            {
                songs = (from x in songs where x.Musician.Name == musicianFilter select x).Include(s => s.Musician).Include(s => s.Genre);
            }
            if (genreFilter != null)
            {
                songs = (from x in songs where x.Genre.GenreName == genreFilter select x).Include(s => s.Musician).Include(s => s.Genre);
            }
            var musicians = _context.Musicians;
            var genres = _context.Genres;
            return View((await songs.ToListAsync(), await musicians.ToListAsync(), await genres.ToListAsync()));
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Musician)
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["MusicianId"] = new SelectList(_context.Musicians, "MusicianId", "MusicianId");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongId,Name,Length,Price,MusicianId")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MusicianId"] = new SelectList(_context.Musicians, "MusicianId", "MusicianId", song.MusicianId);
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["MusicianId"] = new SelectList(_context.Musicians, "MusicianId", "MusicianId", song.MusicianId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongId,Name,Length,Price,MusicianId")] Song song)
        {
            if (id != song.SongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.SongId))
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
            ViewData["MusicianId"] = new SelectList(_context.Musicians, "MusicianId", "MusicianId", song.MusicianId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Musician)
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.SongId == id);
        }
    }
}
