using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICT272_Assignment_3_Online_Tourism_Platform.Data;
using ICT272_Assignment_3_Online_Tourism_Platform.Models;
using Microsoft.AspNetCore.Authorization;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Controllers
{
    public class TravelPackagesBookingController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public TravelPackagesBookingController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: TravelPackagesBooking
        public async Task<IActionResult> Index()
        {
            // var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.GuidedTourBooking.Include(g => g.GuidedToursDate).Include(g => g.User);
            
            var bookings = _context.TravelPackagesBooking
                .Include(b => b.User)
                .Include(b => b.TravelPackagesDate)
                .ThenInclude(gtd => gtd.TravelPackages)
                .Include(b => b.TravelPackagesDate)
                .ThenInclude(gtd => gtd.Agency)
                .ThenInclude(tga => tga.User)
                .Include(b => b.BookingFeedback);

            return View(await bookings.ToListAsync());
            
        }
        
        // POST: TravelPackagesBooking/Personal
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Personal()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(); // or redirect to login
            }

            var bookings = await _context.TravelPackagesBooking
                .Include(b => b.User)
                .Include(b => b.TravelPackagesDate)
                .ThenInclude(gtd => gtd.TravelPackages)
                .Include(b => b.TravelPackagesDate)
                .ThenInclude(gtd => gtd.Agency)
                .ThenInclude(tga => tga.User)
                .Include(b => b.BookingFeedback)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return View(bookings);
        }

        // GET: TravelPackagesBooking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackagesBooking = await _context.TravelPackagesBooking
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackagesBooking == null)
            {
                return NotFound();
            }

            return View(travelPackagesBooking);
        }

        // GET: TravelPackagesBooking/Create
        public IActionResult Create(int? userId, int? guidedTourId)
        {
            var travelPackages = _context.TravelPackages.ToList();
            ViewData["TravelPackages"] = new SelectList(travelPackages, "Id", "Title");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email");

            ViewBag.PreselectedTourId = guidedTourId;
            ViewBag.PreselectedUserId = userId;

            return View();
        }
        
        [HttpGet]
        public JsonResult GetDatesByTourId(int tourId)
        {
            var dates = _context.TravelPackagesDate
                .Where(d => d.TravelPackagesId == tourId)
                .OrderBy(d => d.Date)
                .Select(d => new { d.Id, Date = d.Date.ToString("yyyy-MM-dd") })
                .ToList();

            return Json(dates);
        }

        // POST: TravelPackagesBooking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,TravelPackagesDateId,BookingDate")] TravelPackagesBooking travelPackagesBooking)
        {
            if (ModelState.IsValid)
            {
                travelPackagesBooking.BookingDate = DateTime.Now;
                _context.Add(travelPackagesBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var travelPackagesDate = _context.TravelPackagesDate
                .Include(g => g.TravelPackages)
                .ToList();
            
            ViewData["TravelPackagesDateId"] = new SelectList(_context.TravelPackagesDate, "Id", "TravelPackages.Title", travelPackagesBooking.TravelPackagesDateId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", travelPackagesBooking.UserId);
            return View(travelPackagesBooking);
        }

        // GET: TravelPackagesBooking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackagesBooking = await _context.TravelPackagesBooking
                .Include(g => g.TravelPackagesDate)
                .FirstOrDefaultAsync(g => g.Id == id);
            
            if (travelPackagesBooking == null)
            {
                return NotFound();
            }
            
            ViewBag.PreselectedUserId = travelPackagesBooking.UserId;
            ViewBag.PreselectedTourId = travelPackagesBooking.TravelPackagesDate?.TravelPackagesId;
            
            ViewBag.UserId = new SelectList(_context.User, "Id", "Email", travelPackagesBooking.UserId);
            ViewBag.TravelPackages = new SelectList(_context.TravelPackages, "Id", "Title");

            return View(travelPackagesBooking);
        }

        
        // POST: TravelPackagesBooking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TravelPackagesDateId,BookingDate")] TravelPackagesBooking travelPackagesBooking)
        {
            if (id != travelPackagesBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelPackagesBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelPackagesBookingExists(travelPackagesBooking.Id))
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
            
            var travelPackagesDates = _context.TravelPackagesDate
                .Include(g => g.TravelPackages)
                .ToList();
            
            ViewData["TravelPackagesDatesId"] = new SelectList(_context.TravelPackagesDate, "Id", "TravelPackages.Title", travelPackagesBooking.TravelPackagesDateId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", travelPackagesBooking.UserId);
            
            return View(travelPackagesBooking);
        }

        // GET: TravelPackagesBooking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackagesBooking = await _context.TravelPackagesBooking
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackagesBooking == null)
            {
                return NotFound();
            }

            return View(travelPackagesBooking);
        }

        // POST: TravelPackagesBooking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelPackagesBooking = await _context.TravelPackagesBooking.FindAsync(id);
            if (travelPackagesBooking != null)
            {
                _context.TravelPackagesBooking.Remove(travelPackagesBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelPackagesBookingExists(int id)
        {
            return _context.TravelPackagesBooking.Any(e => e.Id == id);
        }
    }
}
