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
    public class BookingStatusController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public BookingStatusController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: BookingStatus
        public async Task<IActionResult> Index()
        {
            var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.BookingStatus.Include(b => b.GuidedTourBooking).Include(b => b.TravelPackagesBooking);
            return View(await iCT272_Assignment_3_Online_Tourism_PlatformContext.ToListAsync());
        }

        // GET: BookingStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _context.BookingStatus
                .Include(b => b.GuidedTourBooking)
                .Include(b => b.TravelPackagesBooking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingStatus == null)
            {
                return NotFound();
            }

            return View(bookingStatus);
        }

        // GET: BookingStatus/Create
        public IActionResult Create(string bookingType, int bookingId)
        {
            ViewBag.BookingType = bookingType;
            
            // Pass enum options to the view
            ViewBag.ConfirmationStatusList = new SelectList(Enum.GetValues(typeof(Status)));
            ViewBag.PaymentStatusList = new SelectList(Enum.GetValues(typeof(Status)));

            var status = new BookingStatus();

            if (bookingType == "Guided")
            {
                status.GuidedTourBookingId = bookingId;
            }
            else if (bookingType == "Travel")
            {
                status.TravelPackagesBookingId = bookingId;
            }
            
            return View(status);
        }
        

        // POST: BookingStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConfirmationStatus,PaymentStatus,GuidedTourBookingId,TravelPackagesBookingId")] BookingStatus bookingStatus)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(bookingStatus);
                await _context.SaveChangesAsync();
                // Redirect based on booking type
                if (bookingStatus.GuidedTourBookingId.HasValue)
                {
                    return RedirectToAction("Index", "GuidedTourBooking");
                }
                else if (bookingStatus.TravelPackagesBookingId.HasValue)
                {
                    return RedirectToAction("Index", "TravelPackagesBooking");
                }
                else
                {
                    // Fallback if no booking ID is set
                    return RedirectToAction(nameof(Index));
                }
            }
            
            ViewBag.BookingType = bookingStatus.GuidedTourBookingId.HasValue ? "Guided" : "Travel";
            
            
            
            return View(bookingStatus);
        }

// GET: BookingStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _context.BookingStatus.FindAsync(id);
            if (bookingStatus == null)
            {
                return NotFound();
            }

            ViewBag.BookingType = bookingStatus.GuidedTourBookingId.HasValue ? "Guided" : "Travel";

            // Pass enum lists like in Create
            ViewBag.ConfirmationStatusList = new SelectList(Enum.GetValues(typeof(Status)), bookingStatus.ConfirmationStatus);
            ViewBag.PaymentStatusList = new SelectList(Enum.GetValues(typeof(Status)), bookingStatus.PaymentStatus);

            return View(bookingStatus);
        }

// POST: BookingStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConfirmationStatus,PaymentStatus,GuidedTourBookingId,TravelPackagesBookingId")] BookingStatus bookingStatus)
        {
            if (id != bookingStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingStatus);
                    await _context.SaveChangesAsync();

                    // Redirect based on booking type, like Create
                    if (bookingStatus.GuidedTourBookingId.HasValue)
                    {
                        return RedirectToAction("Index", "GuidedTourBooking");
                    }
                    else if (bookingStatus.TravelPackagesBookingId.HasValue)
                    {
                        return RedirectToAction("Index", "TravelPackagesBooking");
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingStatusExists(bookingStatus.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.BookingType = bookingStatus.GuidedTourBookingId.HasValue ? "Guided" : "Travel";

            ViewBag.ConfirmationStatusList = new SelectList(Enum.GetValues(typeof(Status)), bookingStatus.ConfirmationStatus);
            ViewBag.PaymentStatusList = new SelectList(Enum.GetValues(typeof(Status)), bookingStatus.PaymentStatus);

            return View(bookingStatus);
        }


        // GET: BookingStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _context.BookingStatus
                .Include(b => b.GuidedTourBooking)
                .Include(b => b.TravelPackagesBooking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingStatus == null)
            {
                return NotFound();
            }

            return View(bookingStatus);
        }

        // POST: BookingStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingStatus = await _context.BookingStatus.FindAsync(id);
            if (bookingStatus != null)
            {
                _context.BookingStatus.Remove(bookingStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingStatusExists(int id)
        {
            return _context.BookingStatus.Any(e => e.Id == id);
        }
    }
}