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
    public class GuidedTourBookingController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public GuidedTourBookingController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: GuidedTourBooking
        public async Task<IActionResult> Index()
        {
            // var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.GuidedTourBooking.Include(g => g.GuidedToursDate).Include(g => g.User);
            
            var bookings = _context.GuidedTourBooking
                .Include(b => b.User)
                .Include(b => b.GuidedToursDate)
                    .ThenInclude(gtd => gtd.GuidedTours)
                .Include(b => b.GuidedToursDate)
                    .ThenInclude(gtd => gtd.TourGuideAgency)
                        .ThenInclude(tga => tga.User);

            return View(await bookings.ToListAsync());
            
        }

        // GET: GuidedTourBooking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guidedTourBooking = await _context.GuidedTourBooking
                .Include(g => g.GuidedToursDate)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guidedTourBooking == null)
            {
                return NotFound();
            }

            return View(guidedTourBooking);
        }

        // GET: GuidedTourBooking/Create
        public IActionResult Create()
        {
            
            var guidedToursDates = _context.GuidedToursDate
                .Include(g => g.GuidedTours)
                .ToList();
            
            ViewData["GuidedToursDateId"] = new SelectList(_context.GuidedToursDate, "Id", "GuidedTours.Title");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email");
            return View();
        }

        // POST: GuidedTourBooking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,GuidedToursDateId,BookingDate")] GuidedTourBooking guidedTourBooking)
        {
            if (ModelState.IsValid)
            {
                guidedTourBooking.BookingDate = DateTime.Now;
                _context.Add(guidedTourBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var guidedToursDates = _context.GuidedToursDate
                .Include(g => g.GuidedTours)
                .ToList();
            
            ViewData["GuidedToursDateId"] = new SelectList(_context.GuidedToursDate, "Id", "GuidedTours.Title", guidedTourBooking.GuidedToursDateId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", guidedTourBooking.UserId);
            return View(guidedTourBooking);
        }

        // GET: GuidedTourBooking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guidedTourBooking = await _context.GuidedTourBooking.FindAsync(id);
            if (guidedTourBooking == null)
            {
                return NotFound();
            }
            
            var guidedToursDates = _context.GuidedToursDate
                .Include(g => g.GuidedTours)
                .ToList();
            
            ViewData["GuidedToursDateId"] = new SelectList(_context.GuidedToursDate, "Id", "GuidedTours.Title", guidedTourBooking.GuidedToursDateId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", guidedTourBooking.UserId);
            return View(guidedTourBooking);
        }

        // POST: GuidedTourBooking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,GuidedToursDateId,BookingDate")] GuidedTourBooking guidedTourBooking)
        {
            if (id != guidedTourBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guidedTourBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuidedTourBookingExists(guidedTourBooking.Id))
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
            
            var guidedToursDates = _context.GuidedToursDate
                .Include(g => g.GuidedTours)
                .ToList();
            
            ViewData["GuidedToursDateId"] = new SelectList(_context.GuidedToursDate, "Id", "GuidedTours.Title", guidedTourBooking.GuidedToursDateId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", guidedTourBooking.UserId);
            return View(guidedTourBooking);
        }

        // GET: GuidedTourBooking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guidedTourBooking = await _context.GuidedTourBooking
                .Include(g => g.GuidedToursDate)
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guidedTourBooking == null)
            {
                return NotFound();
            }

            return View(guidedTourBooking);
        }

        // POST: GuidedTourBooking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guidedTourBooking = await _context.GuidedTourBooking.FindAsync(id);
            if (guidedTourBooking != null)
            {
                _context.GuidedTourBooking.Remove(guidedTourBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuidedTourBookingExists(int id)
        {
            return _context.GuidedTourBooking.Any(e => e.Id == id);
        }
    }
}
