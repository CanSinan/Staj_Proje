
using Staj_Project.Identity.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staj_Project.APIService.Services.IServices
{
    public interface IAuthService
    {
        Task<AuthServiceResponseDto> SeedRolesAsync();
        Task<AuthServiceResponseDto> CustomerRegisterAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> ExpertRegisterAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);
    }
}
