using System.ComponentModel.DataAnnotations.Schema;
using ICT272_Assignment_3_Online_Tourism_Platform.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Controllers;

public class TourGuideAgency
{
    public int Id { get; set; }              // primary key

    [ForeignKey("User")]
    public int UserId { get; set; }
    [ValidateNever]
    public User User { get; set; }           // navigation to the tour-guide user

    [ForeignKey("Agency")]
    public int AgencyId { get; set; }
    [ValidateNever]
    public Agency Agency { get; set; }       // navigation to the agency
}