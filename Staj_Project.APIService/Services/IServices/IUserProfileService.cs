using Staj_Project.APIService.Models.Profile_Models;

namespace Staj_Project.APIService.Services.IServices
{
    public interface IUserProfileService
    {
        Task<CustomerProfile> CreateCustomerProfileAsync(string userId);
        Task<ExpertProfile> CreateExpertProfileAsync(string userId);
        Task<CustomerProfile> GetCustomerProfileAsync(string id);
        Task<ExpertProfile> GetExpertProfileAsync(string id);

        Task<ServiceResponse> UpdateCustomerProfileAsync(string id, CustomerProfile updatedProfile);
        Task<ServiceResponse> UpdateExpertProfileAsync(string id, ExpertProfile updatedProfile);
    }
}
