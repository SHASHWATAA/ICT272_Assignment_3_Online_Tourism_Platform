using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICT272_Assignment_3_Online_Tourism_Platform.Data;
using ICT272_Assignment_3_Online_Tourism_Platform.Models;
using System.Security.Claims;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Controllers
{
    public class BookingController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public BookingController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.Booking.Include(b => b.Tourist).Include(b => b.TravelPackage);
            return View(await iCT272_Assignment_3_Online_Tourism_PlatformContext.ToListAsync());
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Tourist)
                .Include(b => b.TravelPackage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            ViewData["TouristId"] = new SelectList(_context.Set<Tourist>(), "Id", "Id");
            ViewData["TravelPackageId"] = new SelectList(_context.Set<TravelPackage>(), "Id", "Title");
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TouristId,TravelPackageId,BookingDate,Status,PaymentReceived")] Booking booking)
        {
            string userIdStr = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdStr))
            {
                // User not logged in or claim missing
                ModelState.AddModelError("", "You must be logged in.");
                return View(booking);
            }
            int userId = int.Parse(userIdStr); // Only do this if your UserId is an int

            var tourist = await _context.Tourist.FirstOrDefaultAsync(t => t.UserId == userId);
            if (tourist == null)
            {
                ModelState.AddModelError("", "Tourist profile not found.");
                return View(booking);
            }
            booking.TouristId = tourist.Id;



            ModelState.Remove("Tourist");
            ModelState.Remove("TravelPackage");


            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TouristId"] = new SelectList(_context.Set<Tourist>(), "Id", "Id", booking.TouristId);
            ViewData["TravelPackageId"] = new SelectList(_context.Set<TravelPackage>(), "Id", "Title", booking.TravelPackageId);
            return View(booking);
        }

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["TouristId"] = new SelectList(_context.Set<Tourist>(), "Id", "Id", booking.TouristId);
            ViewData["TravelPackageId"] = new SelectList(_context.Set<TravelPackage>(), "Id", "Title", booking.TravelPackageId);
            return View(booking);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TouristId,TravelPackageId,BookingDate,Status,PaymentReceived")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            ViewData["TouristId"] = new SelectList(_context.Set<Tourist>(), "Id", "Id", booking.TouristId);
            ViewData["TravelPackageId"] = new SelectList(_context.Set<TravelPackage>(), "Id", "Title", booking.TravelPackageId);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Tourist)
                .Include(b => b.TravelPackage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }
    }
}
