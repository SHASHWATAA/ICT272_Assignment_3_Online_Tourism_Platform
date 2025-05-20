using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class Booking
{
    public int Id { get; set; }

    [ForeignKey("Tourist")]
    public int TouristId { get; set; }
    public Tourist Tourist { get; set; }

    [ForeignKey("TravelPackage")]
    public int TravelPackageId { get; set; }
    public TravelPackage TravelPackage { get; set; }

    [Required]
    public DateTime BookingDate { get; set; }

    [Required]
    public string Status { get; set; } // "Pending", "Confirmed", "Completed"

    public bool PaymentReceived { get; set; }
}
