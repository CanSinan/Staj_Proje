using System.ComponentModel.DataAnnotations;

namespace Staj_Project.APIService.Models.Profile_Models
{
    public class CustomerProfile
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public int? PhoneNumber { get; set; }

        public string? City { get; set; }

        public string? District { get; set; }

        public string? Adress { get; set; }

    }
}
