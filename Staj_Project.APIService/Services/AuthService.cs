using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Staj_Project.APIService.Services.IServices;
using Staj_Project.Identity.Core.Dtos;
using Staj_Project.Identity.OtherObject;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Staj_Project.APIService.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserProfileService _profileService;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IUserProfileService profileService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _profileService = profileService;
        }


        public async Task<AuthServiceResponseDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Geçersiz Giriş"
                };

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordCorrect)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Geçersiz Giriş"
                };

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("JWTID", Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateNewJsonWebToken(authClaims);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Token = token
            };
        }

        public async Task<AuthServiceResponseDto> CustomerRegisterAsync(RegisterDto model)
        {
            var isExistsUser = await _userManager.FindByNameAsync(model.UserName);

            if (isExistsUser != null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı zaten kayıtlı"
                };


            IdentityUser newUser = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createUserResult = await _userManager.CreateAsync(newUser, model.Password);

            if (!createUserResult.Succeeded)
            {
                var errorString = "Kullanıcı kayıt olurken sorun oluştu çünkü: ";
                foreach (var error in createUserResult.Errors)
                {
                    errorString += " # " + error.Description;
                }
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = errorString
                };
            }
            AuthServiceResponseDto responseDto = new AuthServiceResponseDto();
            await _userManager.AddToRoleAsync(newUser, StaticUserRoles.CUSTOMER);
            var user = await _userManager.FindByEmailAsync(model.Email);
            await _profileService.CreateCustomerProfileAsync(user.Id);
            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "Kullanıcı Müşteri Olarak Kaydedildi",
                Data = "Id = " + newUser.Id
            };
        }

        public async Task<AuthServiceResponseDto> ExpertRegisterAsync(RegisterDto model)
        {
            var isExistsUser = await _userManager.FindByNameAsync(model.UserName);

            if (isExistsUser != null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı zaten kayıtlı"
                };


            IdentityUser newUser = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createUserResult = await _userManager.CreateAsync(newUser, model.Password);

            if (!createUserResult.Succeeded)
            {
                var errorString = "Kullanıcı kayıt olurken sorun oluştu çünkü: ";
                foreach (var error in createUserResult.Errors)
                {
                    errorString += " # " + error.Description;
                }
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = errorString
                };
            }

            await _userManager.AddToRoleAsync(newUser, StaticUserRoles.EXPERT);
            var user = await _userManager.FindByEmailAsync(model.Email);
            await _profileService.CreateExpertProfileAsync(user.Id);
            return new AuthServiceResponseDto()
            {
                Data = "Id = " + newUser.Id,
                IsSucceed = true,
                Message = "Kullanıcı Uzman Olarak Kaydedildi"
            };
        }

        public async Task<AuthServiceResponseDto> SeedRolesAsync()
        {
            bool isExpertRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.EXPERT);
            bool isCustomerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.CUSTOMER);

            if (isExpertRoleExists && isCustomerRoleExists)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = true,
                    Message = "Roller zaten tanımlı"
                };

            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.EXPERT));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.CUSTOMER));

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "Roller başarılı bir şekilde eklendi"
            };
        }

        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }
    }
}
