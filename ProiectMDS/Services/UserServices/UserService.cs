﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProiectMDS.Exceptions;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Models.Enum;
using ProiectMDS.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProiectMDS.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IS3Service _s3Service;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IS3Service s3Service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _s3Service = s3Service;
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
                linkPozaProfil = user.pozaProfil
            };
            return userInfo;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDTO newUser)
        {

            await _s3Service.UploadFileAsync(newUser.username+"_pfp.png", newUser.pozaProfil);
            var user = new User{
                UserName = newUser.username,
                Email = newUser.email,
                nume = newUser.nume,
                prenume = newUser.prenume,
                PhoneNumber = newUser.nrTelefon,
                permis = "N/A",
                carteIdentitate = "N/A",
                dataNasterii = newUser.dataNasterii,
                pozaProfil = _s3Service.GetFileUrl(newUser.username+"_pfp.png")
            };

            var result = await _userManager.CreateAsync(user,newUser.parola);

            if (result.Succeeded){
                await _userManager.AddToRoleAsync(user, "Propietar");
            }

            return result;
        }
        public async Task<string> LoginAsync(LoginDTO login)
        {
            var user = await _userManager.FindByNameAsync(login.username);
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
                    new Claim(ClaimTypes.Name , user.nume),
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.MobilePhone , user.PhoneNumber),
                    new Claim(ClaimTypes.Role,Role.FirstOrDefault("Default"))
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
