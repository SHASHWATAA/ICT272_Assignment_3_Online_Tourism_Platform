using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class TravelPackagesBooking
{
    public int Id { get; set; }

    // Foreign key to User
    [Required]
    public int UserId { get; set; }
    [ValidateNever]
    public User User { get; set; }

    // Foreign key to TravelPackagesDate
    [Required]
    public int TravelPackagesDateId { get; set; }
    [ValidateNever]
    public TravelPackagesDate TravelPackagesDate { get; set; }

    public DateTime BookingDate { get; set; } = DateTime.Now;
    
    public BookingFeedback? BookingFeedback { get; set; }
    
    public BookingStatus? BookingStatus { get; set; }
    
    

}