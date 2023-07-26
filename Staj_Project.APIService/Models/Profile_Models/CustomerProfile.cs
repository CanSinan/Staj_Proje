using System.ComponentModel.DataAnnotations;

namespace Staj_Project.APIService.Models.Profile_Models
{
    public class CustomerProfile
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; } = null;

        [MaxLength(50)]
        public string? LastName { get; set; } = null;

        [EmailAddress]
        public string? Email { get; set; } = null;

        public int? PhoneNumber { get; set; } = 0;

        public string? City { get; set; } = null;

        public string? District { get; set; } = null;

        public string? Adress { get; set; } = null;

    }
}
