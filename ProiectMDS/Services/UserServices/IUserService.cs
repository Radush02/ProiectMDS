using Microsoft.AspNetCore.Identity;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO newUser);
        Task<UserDTO> getUserDetails(string username);
        Task<string> LoginAsync(LoginDTO login);
    }
}
