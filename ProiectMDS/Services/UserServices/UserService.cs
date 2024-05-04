using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using ProiectMDS.Exceptions;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Models.Enum;
using ProiectMDS.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace ProiectMDS.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IS3Service _s3Service;


        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IS3Service s3Service,IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _s3Service = s3Service;
            _emailSender = emailSender;
        }
        public async Task ConfirmEmail(string username, string token)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new NotFoundException("Userul nu a fost gasit");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new WrongDetailsException("Token invalid");
            }
        }
        public async Task uploadPhoto(RegisterDTO newUser)
        {
            await _s3Service.UploadFileAsync(newUser.username + "_pfp.png", newUser.pozaProfil);
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDTO newUser)
        {

            var user = new User
            {
                UserName = newUser.username,
                Email = newUser.email,
                nume = newUser.nume,
                prenume = newUser.prenume,
                PhoneNumber = newUser.nrTelefon,
                permis = "N/A",
                carteIdentitate = "N/A",
                dataNasterii = newUser.dataNasterii,
                pozaProfil = _s3Service.GetFileUrl(newUser.username + "_pfp.png"),
                puncteFidelitate = 0
            };

            var result = await _userManager.CreateAsync(user, newUser.parola);

            if (result.Succeeded){
                await _userManager.AddToRoleAsync(user, Roles.Default.ToString());
            }

            return result;
        }
        public async Task resetPassword(ResetPasswordDTO user)
        {
            var userByName = await _userManager.FindByNameAsync(user.Username);
            var result = await _userManager.ResetPasswordAsync(userByName, user.Token, user.Password);
            if (!result.Succeeded)
            {
                throw new WrongDetailsException("Token invalid");
            }
        }
        public async Task forgotPassword(ForgotPasswordDTO userDTO)
        {
            var userByName = await _userManager.FindByNameAsync(userDTO.Username);
            var userByEmail = await _userManager.FindByEmailAsync(userDTO.Email);
            if (userByName == null || userByEmail == null)
            {
                throw new NotFoundException("Userul nu a fost gasit");
            }
            if (userByName.Id != userByEmail.Id)
            {
                throw new WrongDetailsException("Userul nu corespunde cu emailul");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(userByName);
            var encodedToken = HttpUtility.UrlEncode(token);
            var url = "https://localhost:4200/resetPassword?username=" + userByName.UserName + "&token=" + encodedToken;
            var emailHtml = @"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Password reset</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        color: #333;
                    }
                    .container {
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #fff;
                        border-radius: 5px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    }
                    .btn {
                        display: inline-block;
                        padding: 10px 20px;
                        background-color: #007bff;
                        color: #fff;
                        text-decoration: none;
                        border-radius: 5px;
                    }
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>Confirmare resetare parola</h2>
                    <p>Confirmati resetarea parolei apasand pe butonul de mai jos. Daca nu ati cerut dvs. acest lucru, puteti ignora mail-ul.</p>
                     <a href='" + url + @"' class='btn'>Resetare parola</a>
                </div>
            </body>
            </html>";
            await _emailSender.SendEmailAsync(userByName.Email, "Resetare parola", emailHtml);
        }
        public async Task sendConfirmationEmail(RegisterDTO newUser)
        {
            User user = await _userManager.FindByNameAsync(newUser.username);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            var url = "https://localhost:7215/api/user/confirmEmail?username=" + user.UserName + "&token=" + encodedToken;
            var emailHtml = @"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Email Confirmation</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        color: #333;
                    }
                    .container {
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #fff;
                        border-radius: 5px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    }
                    .btn {
                        display: inline-block;
                        padding: 10px 20px;
                        background-color: #007bff;
                        color: #fff;
                        text-decoration: none;
                        border-radius: 5px;
                    }
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>Confirmare email</h2>
                    <p>Confirmati adresa de email apasand pe butonul de mai jos:</p>
                    <a href='" + url + @"' class='btn'>Confirma Email</a>
                </div>
            </body>
            </html>
        ";
            await _emailSender.SendEmailAsync(user.Email, "Confirmare email", emailHtml);
        }
        public async Task<UserDTO> getUserDetails (string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                throw new NotFoundException("Userul nu a fost gasit");
            }
            var userInfo = new UserDTO
            {
                username = user.UserName,
                email = user.Email,
                nume = user.nume,
                prenume = user.prenume,
                nrTelefon = user.PhoneNumber,
                permis = user.permis == "N/A" ? false : true,
                carteIdentitate = user.carteIdentitate=="N/A"?false : true,
                dataNasterii = user.dataNasterii,
                linkPozaProfil = user.pozaProfil,
                puncteFidelitate = user.puncteFidelitate
            };
            return userInfo;
        }

        public async Task<string> LoginAsync(LoginDTO login)
        {
            var user = await _userManager.FindByNameAsync(login.username);
            if(user.EmailConfirmed == false)
            {
                throw new NotFoundException("Emailul nu a fost confirmat, va rugam sa verificati emailul pentru link-ul de confirmare");
            }
            var result = await _signInManager.PasswordSignInAsync(login.username,login.parola,login.remember,lockoutOnFailure:false);
            if (result.Succeeded)
            {
                return TokenHandler(user, await _userManager.GetRolesAsync(user));
            }
            else if (result.IsLockedOut)
            {
                throw new LockedOutException("Prea multe incercari de logare in ultima perioada, contul este blocat.");
            }
            else
            {
                throw new WrongDetailsException("User sau parola gresita");
            }
        }
        public string TokenHandler(User user, IList<String> Role)
        {

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier , user.UserName),
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name , user.nume),
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.MobilePhone , user.PhoneNumber),
                    new Claim(ClaimTypes.Role,Role.FirstOrDefault(Roles.Default.ToString()))
                };
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7215/",
                audience: "https://localhost:7215/",
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("rGSSVGNjKoM4qq41wHcssBm4JDzDxfc93rfcAy+id0I=")),
                SecurityAlgorithms.HmacSha256)
                );
            var x = new JwtSecurityTokenHandler().WriteToken(token);
            return x;
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task checkRoleUpdates(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user.permis != "N/A")
            {
                await _userManager.RemoveFromRolesAsync(user,await _userManager.GetRolesAsync(user));
                await _userManager.AddToRoleAsync(user, "Chirias");
            }
            else if(user.carteIdentitate != "N/A")
            {
                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                await _userManager.AddToRoleAsync(user, "Propietar");
            }
        }
        public async Task<IdentityResult> ChangePasswordAsync(UserChangePassDTO user)
        {
            var u = await _userManager.FindByNameAsync(user.username);

            var result = await _userManager.ChangePasswordAsync(u, user.parolaVeche, user.parolaNoua);
            return result;
        }
    }
}
