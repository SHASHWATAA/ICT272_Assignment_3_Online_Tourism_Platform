using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models
{
    public class TravelPackageDate
    {
        public int Id { get; set; }
        [Required]
        public int TravelPackageId { get; set; }
        public TravelPackage TravelPackage { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}