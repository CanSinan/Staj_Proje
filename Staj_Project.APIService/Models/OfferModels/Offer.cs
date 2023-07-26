using System.ComponentModel.DataAnnotations;

namespace Staj_Project.APIService.Models.OfferModels
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
    }
}
