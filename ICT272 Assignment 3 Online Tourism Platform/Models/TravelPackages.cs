using System.ComponentModel.DataAnnotations;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class TravelPackages
{
    public int Id { get; set; }

    [Required] public string Title { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    [Required] public int DurationDays { get; set; }

    [Required] public decimal Price { get; set; }

    [Required] public int MaxGroupSize { get; set; }

}