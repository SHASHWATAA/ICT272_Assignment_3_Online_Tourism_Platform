using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class TravelPackage
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    [Required]
    public int DurationDays { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int MaxGroupSize { get; set; }

    [ForeignKey("Agency")]
    public int AgencyId { get; set; }
    public Agency Agency { get; set; }

    public ICollection<Booking> Bookings { get; set; }

    public ICollection<AvailableDate> AvailableDates { get; set; }

    public ICollection<Feedback> Feedbacks { get; set; }

    public ICollection<TravelPackageDate> TravelPackageDates { get; set; }

}
