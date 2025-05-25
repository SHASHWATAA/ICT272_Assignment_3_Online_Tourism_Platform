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
    public class BookingFeedbackController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public BookingFeedbackController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: BookingFeedback
        public async Task<IActionResult> Index()
        {
            var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.BookingFeedback.Include(b => b.GuidedTourBooking).Include(b => b.TravelPackagesBooking);
            return View(await iCT272_Assignment_3_Online_Tourism_PlatformContext.ToListAsync());
        }

        // GET: BookingFeedback/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingFeedback = await _context.BookingFeedback
                .Include(b => b.GuidedTourBooking)
                .Include(b => b.TravelPackagesBooking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingFeedback == null)
            {
                return NotFound();
            }

            return View(bookingFeedback);
        }

        
        // GET: BookingFeedback/Create
        public IActionResult Create(string bookingType, int bookingId)
        {
            ViewBag.BookingType = bookingType;

            var feedback = new BookingFeedback();

            if (bookingType == "Guided")
            {
                feedback.GuidedTourBookingId = bookingId;
            }
            else if (bookingType == "Travel")
            {
                feedback.TravelPackagesBookingId = bookingId;
            }

            return View(feedback);
        }


        // POST: BookingFeedback/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rating,FeedbackText,GuidedTourBookingId,TravelPackagesBookingId")] BookingFeedback bookingFeedback)
        {
            bookingFeedback.SubmittedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(bookingFeedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Re-pass the correct booking type
            ViewBag.BookingType = bookingFeedback.GuidedTourBookingId.HasValue ? "Guided" : "Travel";
            return View(bookingFeedback);
        }

        // GET: BookingFeedback/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingFeedback = await _context.BookingFeedback.FindAsync(id);
            if (bookingFeedback == null)
            {
                return NotFound();
            }

            ViewBag.BookingType = bookingFeedback.GuidedTourBookingId.HasValue ? "Guided" : "Travel";
            return View(bookingFeedback);
        }


        // POST: BookingFeedback/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rating,FeedbackText,SubmittedAt,GuidedTourBookingId,TravelPackagesBookingId")] BookingFeedback bookingFeedback)
        {
            if (id != bookingFeedback.Id)
            {
                return NotFound();
            }

            // Update submission time
            bookingFeedback.SubmittedAt = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingFeedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingFeedbackExists(bookingFeedback.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Combined", "CombinedBookings");
            }
            ViewData["GuidedTourBookingId"] = new SelectList(_context.GuidedTourBooking, "Id", "Id", bookingFeedback.GuidedTourBookingId);
            ViewData["TravelPackagesBookingId"] = new SelectList(_context.TravelPackagesBooking, "Id", "Id", bookingFeedback.TravelPackagesBookingId);
            return View(bookingFeedback);
        }

        // GET: BookingFeedback/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingFeedback = await _context.BookingFeedback
                .Include(b => b.GuidedTourBooking)
                .Include(b => b.TravelPackagesBooking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingFeedback == null)
            {
                return NotFound();
            }

            return View(bookingFeedback);
        }

        // POST: BookingFeedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingFeedback = await _context.BookingFeedback.FindAsync(id);
            if (bookingFeedback != null)
            {
                _context.BookingFeedback.Remove(bookingFeedback);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingFeedbackExists(int id)
        {
            return _context.BookingFeedback.Any(e => e.Id == id);
        }
    }
}
