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
    public class GuidedToursDateController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public GuidedToursDateController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: GuidedToursDate
        public async Task<IActionResult> Index()
        {
            // var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.GuidedToursDate.Include(g => g.GuidedTours).Include(g => g.TourGuideAgency);
            
            var guidedToursDates = _context.GuidedToursDate
                .Include(g => g.GuidedTours)
                .Include(g => g.TourGuideAgency)
                .ThenInclude(t => t.User);

            
            return View(await guidedToursDates.ToListAsync());
        }

        // GET: GuidedToursDate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guidedToursDate = await _context.GuidedToursDate
                .Include(g => g.GuidedTours)
                .Include(g => g.TourGuideAgency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guidedToursDate == null)
            {
                return NotFound();
            }

            return View(guidedToursDate);
        }

        // GET: GuidedToursDate/Create
        public IActionResult Create()
        {
            ViewData["GuidedToursId"] = new SelectList(_context.GuidedTours, "Id", "Title");
            
            var tourGuideAgencies = _context.TourGuideAgency
                .Include(t => t.User) // Include the User navigation property
                .ToList();
            
            ViewData["TourGuideAgencyId"] = new SelectList(_context.TourGuideAgency, "Id", "User.FullName");
            return View();
        }

        // POST: GuidedToursDate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GuidedToursId,TourGuideAgencyId,Date")] GuidedToursDate guidedToursDate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guidedToursDate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuidedToursId"] = new SelectList(_context.GuidedTours, "Id", "Title", guidedToursDate.GuidedToursId);
            
            var tourGuideAgencies = _context.TourGuideAgency
                .Include(t => t.User) // Include the User navigation property
                .ToList();

            
            ViewData["TourGuideAgencyId"] = new SelectList(_context.TourGuideAgency, "Id", "User.FullName", guidedToursDate.TourGuideAgencyId);
            return View(guidedToursDate);
        }

        // GET: GuidedToursDate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guidedToursDate = await _context.GuidedToursDate.FindAsync(id);
            if (guidedToursDate == null)
            {
                return NotFound();
            }
            ViewData["GuidedToursId"] = new SelectList(_context.GuidedTours, "Id", "Title", guidedToursDate.GuidedToursId);
            
            var tourGuideAgencies = _context.TourGuideAgency
                .Include(t => t.User) // Include the User navigation property
                .ToList();
            
            ViewData["TourGuideAgencyId"] = new SelectList(_context.TourGuideAgency, "Id", "User.FullName", guidedToursDate.TourGuideAgencyId);
            return View(guidedToursDate);
        }

        // POST: GuidedToursDate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GuidedToursId,TourGuideAgencyId,Date")] GuidedToursDate guidedToursDate)
        {
            if (id != guidedToursDate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guidedToursDate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuidedToursDateExists(guidedToursDate.Id))
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
            ViewData["GuidedToursId"] = new SelectList(_context.GuidedTours, "Id", "Title", guidedToursDate.GuidedToursId);
            
            var tourGuideAgencies = _context.TourGuideAgency
                .Include(t => t.User) // Include the User navigation property
                .ToList();
            
            ViewData["TourGuideAgencyId"] = new SelectList(_context.TourGuideAgency, "Id", "User.FullName", guidedToursDate.TourGuideAgencyId);
            return View(guidedToursDate);
        }

        // GET: GuidedToursDate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guidedToursDate = await _context.GuidedToursDate
                .Include(g => g.GuidedTours)
                .Include(g => g.TourGuideAgency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guidedToursDate == null)
            {
                return NotFound();
            }

            return View(guidedToursDate);
        }

        // POST: GuidedToursDate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guidedToursDate = await _context.GuidedToursDate.FindAsync(id);
            if (guidedToursDate != null)
            {
                _context.GuidedToursDate.Remove(guidedToursDate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuidedToursDateExists(int id)
        {
            return _context.GuidedToursDate.Any(e => e.Id == id);
        }
    }
}
