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
    public class AvailableDateController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public AvailableDateController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: AvailableDate
        public async Task<IActionResult> Index()
        {
            var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.AvailableDate.Include(a => a.TravelPackage);
            return View(await iCT272_Assignment_3_Online_Tourism_PlatformContext.ToListAsync());
        }

        // GET: AvailableDate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableDate = await _context.AvailableDate
                .Include(a => a.TravelPackage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (availableDate == null)
            {
                return NotFound();
            }

            return View(availableDate);
        }

        // GET: AvailableDate/Create
        public IActionResult Create()
        {
            ViewData["TravelPackageId"] = new SelectList(_context.Set<TravelPackage>(), "Id", "Title");
            return View();
        }

        // POST: AvailableDate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,TravelPackageId")] AvailableDate availableDate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availableDate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TravelPackageId"] = new SelectList(_context.Set<TravelPackage>(), "Id", "Title", availableDate.TravelPackageId);
            return View(availableDate);
        }

        // GET: AvailableDate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableDate = await _context.AvailableDate.FindAsync(id);
            if (availableDate == null)
            {
                return NotFound();
            }
            ViewData["TravelPackageId"] = new SelectList(_context.Set<TravelPackage>(), "Id", "Title", availableDate.TravelPackageId);
            return View(availableDate);
        }

        // POST: AvailableDate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,TravelPackageId")] AvailableDate availableDate)
        {
            if (id != availableDate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availableDate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailableDateExists(availableDate.Id))
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
            ViewData["TravelPackageId"] = new SelectList(_context.Set<TravelPackage>(), "Id", "Title", availableDate.TravelPackageId);
            return View(availableDate);
        }

        // GET: AvailableDate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableDate = await _context.AvailableDate
                .Include(a => a.TravelPackage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (availableDate == null)
            {
                return NotFound();
            }

            return View(availableDate);
        }

        // POST: AvailableDate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availableDate = await _context.AvailableDate.FindAsync(id);
            if (availableDate != null)
            {
                _context.AvailableDate.Remove(availableDate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailableDateExists(int id)
        {
            return _context.AvailableDate.Any(e => e.Id == id);
        }
    }
}
