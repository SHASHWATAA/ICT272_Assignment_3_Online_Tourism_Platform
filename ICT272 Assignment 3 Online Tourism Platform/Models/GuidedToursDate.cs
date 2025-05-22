using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ICT272_Assignment_3_Online_Tourism_Platform.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class GuidedToursDate
{
    public int Id { get; set; }

    // Foreign key to GuidedTours
    [Required]
    [ForeignKey("GuidedTours")]
    public int GuidedToursId { get; set; }
    [ValidateNever]
    public GuidedTours GuidedTours { get; set; }

    // Foreign key to TourGuideAgency
    [Required]
    [ForeignKey("TourGuideAgency")]
    public int TourGuideAgencyId { get; set; }
    [ValidateNever]
    public TourGuideAgency TourGuideAgency { get; set; }

    // Date field
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
}