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
    public class TravelPackagesController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public TravelPackagesController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: TravelPackages
        public async Task<IActionResult> Index()
        {
            return View(await _context.TravelPackages.ToListAsync());
        }
        
        //Get: TravelPackages/Book
        public async Task<IActionResult> Book()
        {
            var guidedTours = await _context.TravelPackages.ToListAsync();
            return View(guidedTours); // This will look for Views/TravelPackages/Book.cshtml
        }

        // GET: TravelPackages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackages = await _context.TravelPackages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackages == null)
            {
                return NotFound();
            }

            return View(travelPackages);
        }

        // GET: TravelPackages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravelPackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageUrl,DurationDays,Price,MaxGroupSize")] TravelPackages travelPackages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travelPackages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(travelPackages);
        }

        // GET: TravelPackages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackages = await _context.TravelPackages.FindAsync(id);
            if (travelPackages == null)
            {
                return NotFound();
            }
            return View(travelPackages);
        }

        // POST: TravelPackages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,DurationDays,Price,MaxGroupSize")] TravelPackages travelPackages)
        {
            if (id != travelPackages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelPackages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelPackagesExists(travelPackages.Id))
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
            return View(travelPackages);
        }

        // GET: TravelPackages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackages = await _context.TravelPackages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackages == null)
            {
                return NotFound();
            }

            return View(travelPackages);
        }

        // POST: TravelPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelPackages = await _context.TravelPackages.FindAsync(id);
            if (travelPackages != null)
            {
                _context.TravelPackages.Remove(travelPackages);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelPackagesExists(int id)
        {
            return _context.TravelPackages.Any(e => e.Id == id);
        }
    }
}
