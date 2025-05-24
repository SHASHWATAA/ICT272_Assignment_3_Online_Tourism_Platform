using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ICT272_Assignment_3_Online_Tourism_Platform.Models;
using ICT272_Assignment_3_Online_Tourism_Platform.Controllers;

namespace ICT272_Assignment_3_Online_Tourism_Platform.Data
{
    public class ICT272_Assignment_3_Online_Tourism_PlatformContext : DbContext
    {
        public ICT272_Assignment_3_Online_Tourism_PlatformContext (DbContextOptions<ICT272_Assignment_3_Online_Tourism_PlatformContext> options)
            : base(options)
        {
        }

        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.Agency> Agency { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.AvailableDate> AvailableDate { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.Booking> Booking { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.Feedback> Feedback { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.Tourist> Tourist { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.TravelPackage> TravelPackage { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.User> User { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.TravelPackageDate> TravelPackageDate { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Controllers.TourGuideAgency> TourGuideAgency { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.GuidedTours> GuidedTours { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.GuidedToursDate> GuidedToursDate { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.GuidedTourBooking> GuidedTourBooking { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.TravelPackages> TravelPackages { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.TravelPackagesDate> TravelPackagesDate { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.TravelPackagesBooking> TravelPackagesBooking { get; set; } = default!;
        public DbSet<ICT272_Assignment_3_Online_Tourism_Platform.Models.FeaturedGuidedTours> FeaturedGuidedTours { get; set; } = default!;


    }
}
