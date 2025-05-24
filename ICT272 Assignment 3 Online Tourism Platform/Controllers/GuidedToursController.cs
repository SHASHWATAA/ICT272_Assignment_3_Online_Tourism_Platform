using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICT272_Assignment_3_Online_Tourism_Platform.Data;
using ICT272_Assignment_3_Online_Tourism_Platform.Models;
using ICT272_Assignment_3_Online_Tourism_Platform.ViewModels;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Controllers
{
    public class GuidedToursController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public GuidedToursController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }
        

        // GET: GuidedTours
        public async Task<IActionResult> Index()
        {
            var packages = await _context.GuidedTours.ToListAsync();

            var bookingCounts = _context.GuidedTourBooking
                .Include(b => b.GuidedToursDate)
                .GroupBy(b => b.GuidedToursDate.Id)
                .Select(g => new { GuidedTourId = g.Key, Count = g.Count() })
                .ToList();

            var viewModel = packages.Select(pkg =>
            {
                var count = bookingCounts.FirstOrDefault(b => b.GuidedTourId == pkg.Id)?.Count ?? 0;
                return new GuidedToursWithBookingCount
                {
                    Package = pkg,
                    BookingCount = count
                };
            }).ToList();

            return View(viewModel);
        }

        
        //Get: GuidedTours/Book
        public async Task<IActionResult> Book()
        {
            var guidedTours = await _context.GuidedTours.ToListAsync();
            return View(guidedTours); // This will look for Views/GuidedTours/Book.cshtml
        }

        // GET: GuidedTours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guidedTours = await _context.GuidedTours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guidedTours == null)
            {
                return NotFound();
            }

            return View(guidedTours);
        }

        // GET: GuidedTours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GuidedTours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageUrl,DurationDays,Price,MaxGroupSize")] GuidedTours guidedTours)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guidedTours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guidedTours);
        }

        // GET: GuidedTours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guidedTours = await _context.GuidedTours.FindAsync(id);
            if (guidedTours == null)
            {
                return NotFound();
            }
            return View(guidedTours);
        }

        // POST: GuidedTours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,DurationDays,Price,MaxGroupSize")] GuidedTours guidedTours)
        {
            if (id != guidedTours.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guidedTours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuidedToursExists(guidedTours.Id))
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
            return View(guidedTours);
        }

        // GET: GuidedTours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guidedTours = await _context.GuidedTours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guidedTours == null)
            {
                return NotFound();
            }

            return View(guidedTours);
        }

        // POST: GuidedTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guidedTours = await _context.GuidedTours.FindAsync(id);
            if (guidedTours != null)
            {
                _context.GuidedTours.Remove(guidedTours);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuidedToursExists(int id)
        {
            return _context.GuidedTours.Any(e => e.Id == id);
        }
    }
}
