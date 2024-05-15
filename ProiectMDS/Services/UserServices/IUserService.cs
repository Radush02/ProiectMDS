using Microsoft.AspNetCore.Identity;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Models.DTOs.UserDTOs;

namespace ProiectMDS.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO newUser);
        Task<UserDTO> getUserDetails(string username);
        Task<string> LoginAsync(LoginDTO login);
        Task ConfirmEmail(string username, string token);
        Task<bool> uploadPhoto(RegisterDTO newUser);
        Task sendConfirmationEmail(RegisterDTO newUser);
        Task resetPassword(ResetPasswordDTO user);
        Task forgotPassword(ForgotPasswordDTO userDTO);
        Task uploadDocument(string username, string document, IFormFile file);
        Task<SafeUserDTO> getUserProfile(string username);
    }
}
