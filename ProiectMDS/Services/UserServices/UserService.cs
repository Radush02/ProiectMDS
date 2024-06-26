﻿using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using ProiectMDS.Exceptions;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Models.DTOs.UserDTOs;
using ProiectMDS.Models.Enum;
using ProiectMDS.Repositories;
using ProiectMDS.Services;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace ProiectMDS.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IS3Service _s3Service;
        private readonly IPostareRepository _postareRepository;
        private readonly IOpenAIService _openAIService;


        public UserService(UserManager<User> userManager, SignInManager<User> signInManager,IOpenAIService openAIService, IS3Service s3Service,IEmailSender emailSender,IPostareRepository postareRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _s3Service = s3Service;
            _emailSender = emailSender;
            _postareRepository = postareRepository;
            _openAIService = openAIService;
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
        public async Task<bool> uploadPhoto(RegisterDTO newUser)
        {
            string res = (await _openAIService.profilePictureFilter(newUser.pozaProfil)).prompt;
            if(res != "Yes.")
            {
                await failureEmail(newUser, res);
                return false;
            }
            await _s3Service.UploadFileAsync(newUser.username + "_pfp.png", newUser.pozaProfil);
            return true;
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
        public async Task uploadDocument(string username, string document, IFormFile file)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (document == "permis")
            {
                user.permis = _s3Service.GetFileUrl(username + "_permis.png");
            }
            else if (document == "carteIdentitate")
            {
                user.carteIdentitate = _s3Service.GetFileUrl(username + "_carteIdentitate.png");
            }
            else
            {
                throw new WrongDetailsException("Tipul de document nu exista");
            }
            await _userManager.UpdateAsync(user);
            await _s3Service.UploadFileAsync(username + "_" + document + ".png", file);
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
            var url = "http://localhost:4200/resetPassword?username=" + userByName.UserName + "&token=" + encodedToken;
            string emailHtml = await File.ReadAllTextAsync("Templates/ForgotEmailTemplate.html");
            emailHtml = emailHtml.Replace("{{confirmationUrl}}", url);
            emailHtml = emailHtml.Replace("{{username}}", userByName.UserName);
            await _emailSender.SendEmailAsync(userByName.Email, "Resetare parola", emailHtml);
        }
        public async Task sendConfirmationEmail(RegisterDTO newUser)
        {
            User user = await _userManager.FindByNameAsync(newUser.username);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            Console.WriteLine(token);
            var encodedToken = HttpUtility.UrlEncode(token);
            Console.WriteLine(encodedToken);
            var url = "http://localhost:4200/confirmMail?username=" + user.UserName + "&token=" + encodedToken;
            string emailHtml = await File.ReadAllTextAsync("Templates/ConfirmationEmailTemplate.html");
            emailHtml = emailHtml.Replace("{{confirmationUrl}}", url);
            emailHtml = emailHtml.Replace("{{username}}", newUser.username);
            await _emailSender.SendEmailAsync(user.Email, "Confirmare email", emailHtml);
        }
        public async Task failureEmail(RegisterDTO newUser,string reason)
        {
            string emailHtml = await File.ReadAllTextAsync("Templates/FailureEmailTemplate.html");
            emailHtml = emailHtml.Replace("{{username}}", newUser.username);
            emailHtml = emailHtml.Replace("{{reason}}", reason);
            await _emailSender.SendEmailAsync(newUser.email, "Inregistrare esuata", emailHtml);
        }
        public async Task<SafeUserDTO> getUserProfile(string username)
        {
            var u = await _userManager.FindByNameAsync(username) ?? throw new NotFoundException("Userul nu a fost gasit");
            int nrPostari = await _postareRepository.NrPostareByUser(u.Id);
            Console.WriteLine(nrPostari);
            return new SafeUserDTO()
            { 
                id=u.Id,
                nume = u.nume,
                prenume = u.prenume,
                username = u.UserName,
                nrTelefon = u.PhoneNumber,
                linkPozaProfil = u.pozaProfil,
                dataNasterii = u.dataNasterii,
                nrPostari = nrPostari
            };
        }
        public async Task<UserDTO> getUserDetails (string username)
        {
            var user = await _userManager.FindByNameAsync(username) ?? throw new NotFoundException("Userul nu a fost gasit");
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
            if (user == null)
            {
                throw new NotFoundException("Nu exista userul");
            }
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
        public async Task<UserDTO> getUserById(int id)
        {
            var u = await _userManager.FindByIdAsync(id.ToString());
            if (u == null)
            {
                throw new NotFoundException("Userul nu a fost gasit");
            }
            var userInfo = new UserDTO
            {
                username = u.UserName,
                email = u.Email,
                nume = u.nume,
                prenume = u.prenume,
                nrTelefon = u.PhoneNumber,
                permis = u.permis == "N/A" ? false : true,
                carteIdentitate = u.carteIdentitate == "N/A" ? false : true,
                dataNasterii = u.dataNasterii,
                linkPozaProfil = u.pozaProfil,
                puncteFidelitate = u.puncteFidelitate
            };
            return userInfo;
        }
    }
}
