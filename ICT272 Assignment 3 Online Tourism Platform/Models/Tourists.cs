using System.ComponentModel.DataAnnotations.Schema;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class Tourist
{
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<Booking> Bookings { get; set; }

    public ICollection<Feedback> Feedbacks { get; set; }
}
