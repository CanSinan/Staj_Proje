using Microsoft.AspNetCore.Identity;
using Staj_Project.APIService.Models.OfferModels;
using Staj_Project.APIService.Models.Profile_Models;
using Staj_Project.Identity.Core.Dtos;

namespace Staj_Project.APIService.Services.IServices
{
    public interface IOfferService
    {
        Task<CustomerProfile> SendOffer(string userId, Offer offer, string selectedExpertId);
        Task<List<Offer>> GetOfferFromCustomer(string userId);
        Task<List<Offer>> GetOfferForExpertFromCustomer(string userId);
        Task<CustomerProfile> GetCustomerById(string userId);
        Task<ExpertProfile> GetExpertById(string userId);

    }
}
