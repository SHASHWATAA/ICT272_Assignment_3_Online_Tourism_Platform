using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ICT272_Assignment_3_Online_Tourism_Platform.Data; // Make sure this is included
using ICT272_Assignment_3_Online_Tourism_Platform.Models;
using ICT272_Assignment_3_Online_Tourism_Platform.ViewModels;
using Microsoft.AspNetCore.Authorization;

public class CombinedBookingsController : Controller
{
    private readonly ICT272_Assignment_3_Online_Tourism_PlatformContext _context;

    public CombinedBookingsController(ICT272_Assignment_3_Online_Tourism_PlatformContext context)
    {
        _context = context;
    }

    [Authorize]
    public IActionResult Combined()
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;
    
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            return Unauthorized(); // or redirect to login
        }
    
        var guidedTourBookings = _context.GuidedTourBooking
            .Include(g => g.User)
            .Include(g => g.GuidedToursDate)
                .ThenInclude(d => d.GuidedTours)
            .Include(g => g.GuidedToursDate.TourGuideAgency)
                .ThenInclude(a => a.User)
            .Where(g => g.UserId == userId)
            .ToList();
    
        var travelPackageBookings = _context.TravelPackagesBooking
            .Include(t => t.User)
            .Include(t => t.TravelPackagesDate)
                .ThenInclude(d => d.TravelPackages)
            .Include(t => t.TravelPackagesDate.Agency)
                .ThenInclude(a => a.User)
            .Where(t => t.UserId == userId)
            .ToList();
    
        var viewModel = new CombinedBookingsViewModel
        {
            GuidedTourBookings = guidedTourBookings,
            TravelPackageBookings = travelPackageBookings
        };
    
        return View(viewModel);
    }

}