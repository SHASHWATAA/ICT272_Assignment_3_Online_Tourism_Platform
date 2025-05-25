using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public enum Status
{
    Pending,
    Confirmed,
    Completed
}

public class BookingStatus
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Status ConfirmationStatus { get; set; }

    [Required]
    public Status PaymentStatus { get; set; }
    
    // Optional link to GuidedTourBooking
    public int? GuidedTourBookingId { get; set; }
    [ValidateNever]
    public GuidedTourBooking? GuidedTourBooking { get; set; }

    // Optional link to TravelPackagesBooking
    public int? TravelPackagesBookingId { get; set; }
    [ValidateNever]
    public TravelPackagesBooking? TravelPackagesBooking { get; set; }
    
}