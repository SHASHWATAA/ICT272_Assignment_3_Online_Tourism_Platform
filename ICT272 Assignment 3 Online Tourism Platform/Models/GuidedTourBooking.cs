using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class GuidedTourBooking
{
    public int Id { get; set; }

    // Foreign key to User
    [Required]
    public int UserId { get; set; }
    [ValidateNever]
    public User User { get; set; }

    // Foreign key to GuidedToursDate
    [Required]
    public int GuidedToursDateId { get; set; }
    [ValidateNever]
    public GuidedToursDate GuidedToursDate { get; set; }

    public DateTime BookingDate { get; set; } = DateTime.Now;
}