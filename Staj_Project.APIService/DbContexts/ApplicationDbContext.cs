using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Staj_Project.APIService.Models.Profile_Models;

namespace Staj_Project.APIService.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CustomerProfile> CustomProfiles { get; set; }
        public DbSet<ExpertProfile> ExpertProfiles { get; set; }
    }
}
