using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class Feedback
{
    public int Id { get; set; }

    [ForeignKey("Tourist")]
    public int TouristId { get; set; }
    public Tourist Tourist { get; set; }

    [ForeignKey("TravelPackage")]
    public int TravelPackageId { get; set; }
    public TravelPackage TravelPackage { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    public string Comment { get; set; }

    public DateTime SubmittedAt { get; set; }
}
