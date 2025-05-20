using System.ComponentModel.DataAnnotations;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; } // "Tourist", "Agency"
}