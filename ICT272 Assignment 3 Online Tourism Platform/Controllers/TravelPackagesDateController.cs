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
    public class TravelPackagesDateController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public TravelPackagesDateController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: TravelPackagesDate
        public async Task<IActionResult> Index()
        {
            var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.TravelPackagesDate.Include(t => t.Agency).Include(t => t.TravelPackages);
            return View(await iCT272_Assignment_3_Online_Tourism_PlatformContext.ToListAsync());
        }

        // GET: TravelPackagesDate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackagesDate = await _context.TravelPackagesDate
                .Include(t => t.Agency)
                .Include(t => t.TravelPackages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackagesDate == null)
            {
                return NotFound();
            }

            return View(travelPackagesDate);
        }

        // GET: TravelPackagesDate/Create
        public IActionResult Create()
        {
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "AgencyName");
            ViewData["TravelPackagesId"] = new SelectList(_context.TravelPackages, "Id", "Title");
            return View();
        }

        // POST: TravelPackagesDate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TravelPackagesId,AgencyId,Date")] TravelPackagesDate travelPackagesDate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travelPackagesDate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "Id", travelPackagesDate.AgencyId);
            ViewData["TravelPackagesId"] = new SelectList(_context.TravelPackages, "Id", "Title", travelPackagesDate.TravelPackagesId);
            return View(travelPackagesDate);
        }

        // GET: TravelPackagesDate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackagesDate = await _context.TravelPackagesDate.FindAsync(id);
            if (travelPackagesDate == null)
            {
                return NotFound();
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "Id", travelPackagesDate.AgencyId);
            ViewData["TravelPackagesId"] = new SelectList(_context.TravelPackages, "Id", "Title", travelPackagesDate.TravelPackagesId);
            return View(travelPackagesDate);
        }

        // POST: TravelPackagesDate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TravelPackagesId,AgencyId,Date")] TravelPackagesDate travelPackagesDate)
        {
            if (id != travelPackagesDate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelPackagesDate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelPackagesDateExists(travelPackagesDate.Id))
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
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "Id", travelPackagesDate.AgencyId);
            ViewData["TravelPackagesId"] = new SelectList(_context.TravelPackages, "Id", "Title", travelPackagesDate.TravelPackagesId);
            return View(travelPackagesDate);
        }

        // GET: TravelPackagesDate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackagesDate = await _context.TravelPackagesDate
                .Include(t => t.Agency)
                .Include(t => t.TravelPackages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackagesDate == null)
            {
                return NotFound();
            }

            return View(travelPackagesDate);
        }

        // POST: TravelPackagesDate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelPackagesDate = await _context.TravelPackagesDate.FindAsync(id);
            if (travelPackagesDate != null)
            {
                _context.TravelPackagesDate.Remove(travelPackagesDate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelPackagesDateExists(int id)
        {
            return _context.TravelPackagesDate.Any(e => e.Id == id);
        }
    }
}
