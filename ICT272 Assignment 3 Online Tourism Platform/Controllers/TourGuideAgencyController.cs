using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICT272_Assignment_3_Online_Tourism_Platform.Data;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Controllers
{
    public class TourGuideAgencyController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public TourGuideAgencyController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: TourGuideAgency
        public async Task<IActionResult> Index()
        {
            var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.TourGuideAgency.Include(t => t.Agency).Include(t => t.User);
            return View(await iCT272_Assignment_3_Online_Tourism_PlatformContext.ToListAsync());
        }

        // GET: TourGuideAgency/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourGuideAgency = await _context.TourGuideAgency
                .Include(t => t.Agency)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourGuideAgency == null)
            {
                return NotFound();
            }

            return View(tourGuideAgency);
        }

        // GET: TourGuideAgency/Create
        public IActionResult Create()
        {
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "AgencyName");
            ViewData["UserId"] = new SelectList(_context.User.Where(u => u.Role == "TourGuide"), "Id", "FullName");
            return View();
        }

        // POST: TourGuideAgency/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,AgencyId")] TourGuideAgency tourGuideAgency)
        {
            foreach (var error in ModelState)
            {
                Console.WriteLine($"Key: {error.Key}");
                foreach (var err in error.Value.Errors)
                {
                    Console.WriteLine($"Error: {err.ErrorMessage}");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(tourGuideAgency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "AgencyName", tourGuideAgency.AgencyId);
            ViewData["UserId"] = new SelectList(_context.User.Where(u => u.Role == "TourGuide"), "Id", "FullName", tourGuideAgency.UserId);
            return View(tourGuideAgency);
        }

        // GET: TourGuideAgency/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourGuideAgency = await _context.TourGuideAgency.FindAsync(id);
            if (tourGuideAgency == null)
            {
                return NotFound();
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "AgencyName", tourGuideAgency.AgencyId);
            ViewData["UserId"] = new SelectList(_context.User.Where(u => u.Role == "TourGuide"), "Id", "FullName", tourGuideAgency.UserId);
            return View(tourGuideAgency);
        }

        // POST: TourGuideAgency/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,AgencyId")] TourGuideAgency tourGuideAgency)
        {
            if (id != tourGuideAgency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourGuideAgency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourGuideAgencyExists(tourGuideAgency.Id))
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
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "AgencyName", tourGuideAgency.AgencyId);
            ViewData["UserId"] = new SelectList(_context.User.Where(u => u.Role == "TourGuide"), "Id", "FullName", tourGuideAgency.UserId);
            return View(tourGuideAgency);
        }

        // GET: TourGuideAgency/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourGuideAgency = await _context.TourGuideAgency
                .Include(t => t.Agency)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourGuideAgency == null)
            {
                return NotFound();
            }

            return View(tourGuideAgency);
        }

        // POST: TourGuideAgency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourGuideAgency = await _context.TourGuideAgency.FindAsync(id);
            if (tourGuideAgency != null)
            {
                _context.TourGuideAgency.Remove(tourGuideAgency);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourGuideAgencyExists(int id)
        {
            return _context.TourGuideAgency.Any(e => e.Id == id);
        }
    }
}
