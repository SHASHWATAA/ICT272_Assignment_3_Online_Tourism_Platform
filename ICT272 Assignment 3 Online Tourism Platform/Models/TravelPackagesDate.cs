using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ICT272_Assignment_3_Online_Tourism_Platform.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class TravelPackagesDate
{
    public int Id { get; set; }

    // Foreign key to TravelPackages
    [Required]
    [ForeignKey("TravelPackages")]
    public int TravelPackagesId { get; set; }
    [ValidateNever]
    public TravelPackages TravelPackages { get; set; }

    // Foreign key to Agency
    [Required]
    [ForeignKey("Agency")]
    public int AgencyId { get; set; }
    [ValidateNever]
    public Agency Agency { get; set; }

    // Date field
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
}