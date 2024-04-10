using Microsoft.AspNetCore.Identity;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO newUser);
        Task<string> LoginAsync(LoginDTO login);
    }
}
