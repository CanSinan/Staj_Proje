using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Staj_Project.APIService.Models.OfferModels;
using Staj_Project.APIService.Models.Profile_Models;
using Staj_Project.Identity.Core.Dtos;

namespace Staj_Project.APIService.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CustomerProfile> CustomProfiles { get; set; }
        public DbSet<ExpertProfile> ExpertProfiles { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Offer> Offers { get; set; }
    
    }
}
