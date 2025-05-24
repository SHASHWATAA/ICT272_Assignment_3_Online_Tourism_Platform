namespace ICT272_Assignment_3_Online_Tourism_Platform.Models;

public class HomeIndexViewModel
{
    public List<FeaturedGuidedTours> FeaturedTours { get; set; }
    public List<GuidedToursDate> UpcomingTours { get; set; }
    
    public List<User> User { get; set; }
}