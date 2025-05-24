// Models/ViewModels/CombinedBookingsViewModel.cs
using System.Collections.Generic;
using ICT272_Assignment_3_Online_Tourism_Platform.Models;

namespace ICT272_Assignment_3_Online_Tourism_Platform.ViewModels;

public class CombinedBookingsViewModel
{
    public IEnumerable<GuidedTourBooking> GuidedTourBookings { get; set; }
    public IEnumerable<TravelPackagesBooking> TravelPackageBookings { get; set; }
}