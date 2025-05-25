using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class FeaturedGuidedTours
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int GuidedTourId { get; set; }
    
    [ValidateNever]
    [ForeignKey("GuidedTourId")]
    public GuidedTours GuidedTour { get; set; }

    public bool IsFeatured { get; set; }
}