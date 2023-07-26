using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Staj_Project.APIService.DbContexts;
using Staj_Project.APIService.Models.Profile_Models;
using Staj_Project.APIService.Services.IServices;
using System.Data;

namespace Staj_Project.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileAPIController : ControllerBase
    {
        private readonly IUserProfileService _profileService;
        private readonly ApplicationDbContext _context;

        public UserProfileAPIController(IUserProfileService profileService, ApplicationDbContext context)
        {
            _profileService = profileService;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "CUSTOMER")]
        [Route("customer/{id}")]
        public async Task<IActionResult> GetCustomerProfile(string id)
        {
            var profile = await _profileService.GetCustomerProfileAsync(id);

            if (profile == null)
            {
                return Ok(profile);
            }

            return Ok(profile);
        }

        [HttpPost]
        [Route("customer/update/{id}")]

        public async Task<IActionResult> UpdateCustomerProfile(string id, [FromBody] CustomerProfile updatedProfile)
        {
            var profile = await _profileService.UpdateCustomerProfileAsync(id, updatedProfile);

            if (profile.IsSucceed == false)
            {
                return NotFound("Profil bulunamadı.");
            }

            return Ok(profile);
        }


        [HttpGet]
        [Route("expert/{id}")]
        public async Task<IActionResult> GetExpertProfile(string id)
        {
            var profile = await _profileService.GetExpertProfileAsync(id);

            if (profile == null)
            {
                return NotFound("Profil bulunamadı.");
            }

            return Ok(profile);
        }

        [HttpPost]
        [Route("expert/update/{id}")]

        public async Task<IActionResult> UpdateExpertProfile(string id, [FromBody] ExpertProfile updatedProfile)
        {
            var profile = await _profileService.UpdateExpertProfileAsync(id, updatedProfile);

            if (profile.IsSucceed == false)
            {
                return BadRequest("Profil bulunamadı.");
            }

            return Ok(profile);
        }
    }
}
