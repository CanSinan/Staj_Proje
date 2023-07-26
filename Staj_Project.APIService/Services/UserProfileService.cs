using Staj_Project.APIService.DbContexts;
using Staj_Project.APIService.Models.Profile_Models;
using Staj_Project.APIService.Services.IServices;

namespace Staj_Project.APIService.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserProfileService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CustomerProfile> CreateCustomerProfileAsync(string userId)
        {
            var profile = new CustomerProfile
            {

                Id = userId,
            };

            _dbContext.CustomProfiles.Add(profile);
            await _dbContext.SaveChangesAsync();

            return profile;
        }
        public async Task<ExpertProfile> CreateExpertProfileAsync(string userId)
        {
            var profile = new ExpertProfile
            {
                Id = userId,
            };

            _dbContext.ExpertProfiles.Add(profile);
            await _dbContext.SaveChangesAsync();

            return profile;
        }

        public async Task<CustomerProfile> GetCustomerProfileAsync(string id)
        {
            var response = await _dbContext.CustomProfiles.FindAsync(id);
            if (response == null)
            {
                return response;

            }
            return response;

        }

        public async Task<ExpertProfile> GetExpertProfileAsync(string id)
        {
            var response = await _dbContext.ExpertProfiles.FindAsync(id);
            if (response == null)
            {
                return response;

            }
            return response;

        }

        public async Task<ServiceResponse> UpdateCustomerProfileAsync(string id, CustomerProfile updatedProfile)
        {
            var profile = await _dbContext.CustomProfiles.FindAsync(id);
            ServiceResponse response = new ServiceResponse();
            if (profile == null)
            {
                return new ServiceResponse()
                {
                    IsSucceed = false,
                    Message = "Profil Bulunamadı"
                };
            }

            // Profil güncelleme işlemleri burada yapılacak.
            profile.FirstName = updatedProfile.FirstName;
            profile.LastName = updatedProfile.LastName;
            profile.Email = updatedProfile.Email;
            profile.PhoneNumber = updatedProfile.PhoneNumber;
            profile.Adress = updatedProfile.Adress;
            profile.City = updatedProfile.City;
            profile.District = updatedProfile.District;

            await _dbContext.SaveChangesAsync();
            response.Data = profile;
            return new ServiceResponse()
            {
                IsSucceed = true,
                Message = "Profil Oluşturuldu."
            };
        }

        public async Task<ServiceResponse> UpdateExpertProfileAsync(string id, ExpertProfile updatedProfile)
        {
            var profile = await _dbContext.ExpertProfiles.FindAsync(id);
            ServiceResponse response = new ServiceResponse();
            if (profile == null)
            {
                response.IsSucceed = false;
                response.Message = "Profil Bulunamadı";
                return response;
            }

            // Profil güncelleme işlemleri burada yapılacak.
            profile.Profession = updatedProfile.Profession;
            profile.FirstName = updatedProfile.FirstName;
            profile.LastName = updatedProfile.LastName;
            profile.Email = updatedProfile.Email;
            profile.PhoneNumber = updatedProfile.PhoneNumber;
            profile.Adress = updatedProfile.Adress;
            profile.City = updatedProfile.City;
            profile.District = updatedProfile.District;

            await _dbContext.SaveChangesAsync();
            response.Data = profile;
            response.IsSucceed = true;
            response.Message = "Profil Güncellendi.";
            return response;
        }

    }
}
