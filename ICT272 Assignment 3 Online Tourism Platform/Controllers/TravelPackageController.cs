using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICT272_Assignment_3_Online_Tourism_Platform.Data;
using ICT272_Assignment_3_Online_Tourism_Platform.Models;
using System.Diagnostics;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Controllers
{
    public class TravelPackageController : Controller
    {
        private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

        public TravelPackageController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
        {
            _context = context;
        }

        // GET: TravelPackage
        public async Task<IActionResult> Index()
        {
            var iCT272_Assignment_3_Online_Tourism_PlatformContext = _context.TravelPackage.Include(t => t.Agency);
            return View(await iCT272_Assignment_3_Online_Tourism_PlatformContext.ToListAsync());
        }

        // GET: TravelPackage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackage
                .Include(t => t.Agency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // GET: TravelPackage/Create
        public IActionResult Create()
        {
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "AgencyName");
            return View();
        }

        // POST: TravelPackage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Description,ImageUrl,DurationDays,Price,MaxGroupSize,AgencyId")] TravelPackage travelPackage,
            string travelPackageDates)
        {
            // Remove navigation property validation
            ModelState.Remove("AvailableDates");
            ModelState.Remove("Feedbacks");
            ModelState.Remove("Bookings");
            ModelState.Remove("Agency");
            ModelState.Remove("TravelPackageDates");


           


            if (ModelState.IsValid)
            {
                _context.Add(travelPackage);
                await _context.SaveChangesAsync();

                // Parse and save TravelPackageDates
                if (!string.IsNullOrWhiteSpace(travelPackageDates))
                {
                    var dateStrings = travelPackageDates.Split(',');
                    foreach (var ds in dateStrings)
                    {
                        if (DateTime.TryParse(ds.Trim(), out var date))
                        {
                            _context.TravelPackageDate.Add(new TravelPackageDate
                            {
                                TravelPackageId = travelPackage.Id,
                                Date = date
                            });
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "AgencyName", travelPackage.AgencyId);
            return View(travelPackage);
        }


        // GET: TravelPackage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackage.FindAsync(id);
            if (travelPackage == null)
            {
                return NotFound();
            }
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "AgencyName", travelPackage.AgencyId);
            return View(travelPackage);
        }

        // POST: TravelPackage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,DurationDays,Price,MaxGroupSize,AgencyId")] TravelPackage travelPackage)
        {
            if (id != travelPackage.Id)
            {
                return NotFound();
            }


            ModelState.Remove("AvailableDates");  // if removed, will cause validation error with db
            ModelState.Remove("Feedbacks");    // if removed, will cause validation error with db
            ModelState.Remove("Bookings");    // if removed, will cause validation error with db
            ModelState.Remove("Agency");      // if removed, will cause validation error with db , you can use ModelState.Remove similarly if (ModelState.IsValid) is being false and not reading keys from the model, not professional fix, but will do for our purposes


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelPackage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelPackageExists(travelPackage.Id))
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
            ViewData["AgencyId"] = new SelectList(_context.Agency, "Id", "AgencyName", travelPackage.AgencyId);
            return View(travelPackage);
        }

        // GET: TravelPackage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackage
                .Include(t => t.Agency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // POST: TravelPackage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelPackage = await _context.TravelPackage.FindAsync(id);
            if (travelPackage != null)
            {
                _context.TravelPackage.Remove(travelPackage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AdminIndex()
        {
            var travelPackages = _context.TravelPackage.Include(t => t.Agency);
            return View(await travelPackages.ToListAsync());
        }



        private bool TravelPackageExists(int id)
        {
            return _context.TravelPackage.Any(e => e.Id == id);
        }





    }
}
