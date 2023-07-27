using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Staj_Project.APIService.Models.OfferModels;
using Staj_Project.APIService.Models.Profile_Models;
using Staj_Project.APIService.Services.IServices;
using Staj_Project.Identity.Core.Dtos;

namespace Staj_Project.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferAPIController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferAPIController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpPost]
        [Authorize(Roles = "CUSTOMER")]
        [Route("sendoffer")]
        public async Task<IActionResult> SendOffer(string userId, [FromBody] Offer offer, string selectedExpertId)
        {
            CustomerProfile user = await _offerService.SendOffer(userId, offer, selectedExpertId);
            if (user == null) 
            {
                return NotFound("Teklif gönderilemedi."); // Kullanıcı bulunamazsa hata dönecek.
            }

            return Ok(offer);
        }

        [HttpGet]
        [Authorize(Roles ="CUSTOMER")]
        [Route("get/customer/offers")]

        public async Task<IActionResult> GetOfferFromCustomerAsync(string userId)
        {
            var customerProfile = await _offerService.GetCustomerById(userId);

            if (customerProfile == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var offers = await _offerService.GetOfferFromCustomer(userId);

            if (offers == null )
            {
                return NotFound("Teklif yok.");
            }

            return Ok(new { Offers = offers });
        }

        [HttpGet]
        [Authorize(Roles = "EXPERT")]
        [Route("get/offers/from/customer/forExpert")]

        public async Task<IActionResult> GetOfferForExperFromCustomerAsync(string userId)
        {
            var expertProfile = await _offerService.GetExpertById(userId);

            if (expertProfile == null)
            {
                return NotFound("Uzman bulunamadı.");
            }

            var offers = await _offerService.GetOfferForExpertFromCustomer(userId);

            if (offers == null )
            {
                return NotFound("Teklif yok.");
            }

            return Ok(new {Offers = offers });
        }
    }
}
