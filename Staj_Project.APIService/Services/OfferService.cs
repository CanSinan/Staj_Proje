using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Staj_Project.APIService.DbContexts;
using Staj_Project.APIService.Models.OfferModels;
using Staj_Project.APIService.Models.Profile_Models;
using Staj_Project.APIService.Services.IServices;
using Staj_Project.Identity.Core.Dtos;

namespace Staj_Project.APIService.Services
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _context;

        public OfferService( ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ExpertProfile> IsExpertIdValid(string selectedExpertId)
        {
            // AspNetUsers tablosundaki kullanıcılar arasında Id'si selectedExpertId ile eşleşen bir kullanıcıyı getirmesi lazım.
            return await _context.ExpertProfiles.FindAsync(selectedExpertId);
        }
        public async Task<CustomerProfile> SendOffer(string userId,[FromBody]Offer offer,string selectedExpertId)
        {
            CustomerProfile user = await GetCustomerById(userId);
            if (user == null)
            {
                return null;
            }
            ExpertProfile user2 = await GetExpertById(selectedExpertId);

            if (user2 == null)
            {
                return null;
            }

            offer.SenderId = userId;
            offer.ReceiverId = selectedExpertId;
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<List<Offer>> GetOfferForExpertFromCustomer(string userId)
        {
            // Uzmanın aldığı teklifleri çekiyoruz.
            var offers = await _context.Offers.Where(o => o.ReceiverId == userId).ToListAsync();
            return offers;
        }
        public async Task<List<Offer>> GetOfferFromCustomer(string userId)
        {

            var offers = await _context.Offers.Where(o => o.SenderId == userId).ToListAsync();
            return offers;
        }

        public async Task<CustomerProfile> GetCustomerById(string userId)
        {
            try
            {
                return await _context.CustomProfiles.FindAsync(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<ExpertProfile> GetExpertById(string userId)
        {
            try
            {
                return await _context.ExpertProfiles.FindAsync(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
