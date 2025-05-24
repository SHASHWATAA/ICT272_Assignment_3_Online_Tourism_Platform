using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICT272_Assignment_3_Online_Tourism_Platform.Data;
using ICT272_Assignment_3_Online_Tourism_Platform.Models;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Controllers
{
    public class FeaturedGuidedToursController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public FeaturedGuidedToursController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: FeaturedGuidedTours
        public async Task<IActionResult> Index()
        {
            var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.FeaturedGuidedTours.Include(f => f.GuidedTour);
            return View(await iCT272_Assignment_3_Online_Tourism_PlatformContext.ToListAsync());
        }

        // GET: FeaturedGuidedTours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featuredGuidedTours = await _context.FeaturedGuidedTours
                .Include(f => f.GuidedTour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featuredGuidedTours == null)
            {
                return NotFound();
            }

            return View(featuredGuidedTours);
        }

        // GET: FeaturedGuidedTours/Create
        public IActionResult Create()
        {
            ViewData["GuidedTourId"] = new SelectList(_context.GuidedTours, "Id", "Title");
            return View();
        }

        // POST: FeaturedGuidedTours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GuidedTourId,IsFeatured")] FeaturedGuidedTours featuredGuidedTours)
        {
            if (ModelState.IsValid)
            {
                _context.Add(featuredGuidedTours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuidedTourId"] = new SelectList(_context.GuidedTours, "Id", "Title", featuredGuidedTours.GuidedTourId);
            return View(featuredGuidedTours);
        }

        // GET: FeaturedGuidedTours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featuredGuidedTours = await _context.FeaturedGuidedTours.FindAsync(id);
            if (featuredGuidedTours == null)
            {
                return NotFound();
            }
            ViewData["GuidedTourId"] = new SelectList(_context.GuidedTours, "Id", "Title", featuredGuidedTours.GuidedTourId);
            return View(featuredGuidedTours);
        }

        // POST: FeaturedGuidedTours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GuidedTourId,IsFeatured")] FeaturedGuidedTours featuredGuidedTours)
        {
            if (id != featuredGuidedTours.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(featuredGuidedTours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeaturedGuidedToursExists(featuredGuidedTours.Id))
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
            ViewData["GuidedTourId"] = new SelectList(_context.GuidedTours, "Id", "Title", featuredGuidedTours.GuidedTourId);
            return View(featuredGuidedTours);
        }

        // GET: FeaturedGuidedTours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featuredGuidedTours = await _context.FeaturedGuidedTours
                .Include(f => f.GuidedTour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featuredGuidedTours == null)
            {
                return NotFound();
            }

            return View(featuredGuidedTours);
        }

        // POST: FeaturedGuidedTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var featuredGuidedTours = await _context.FeaturedGuidedTours.FindAsync(id);
            if (featuredGuidedTours != null)
            {
                _context.FeaturedGuidedTours.Remove(featuredGuidedTours);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeaturedGuidedToursExists(int id)
        {
            return _context.FeaturedGuidedTours.Any(e => e.Id == id);
        }
    }
}
