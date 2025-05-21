using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class Agency
{
    public int Id { get; set; }

    
    public string AgencyName { get; set; }

    public string Description { get; set; }

    public string ServicesOffered { get; set; }

    public string ImageUrl { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<TravelPackage> TravelPackages { get; set; }
}
