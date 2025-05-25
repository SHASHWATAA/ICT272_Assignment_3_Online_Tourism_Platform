using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class BookingFeedback
{
    public int Id { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [StringLength(1000)]
    public string? FeedbackText { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.Now;

    // Optional link to GuidedTourBooking
    public int? GuidedTourBookingId { get; set; }
    [ValidateNever]
    public GuidedTourBooking? GuidedTourBooking { get; set; }

    // Optional link to TravelPackagesBooking
    public int? TravelPackagesBookingId { get; set; }
    [ValidateNever]
    public TravelPackagesBooking? TravelPackagesBooking { get; set; }
}