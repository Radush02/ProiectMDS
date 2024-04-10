using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ProiectMDS.Exceptions;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Services;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO user)
        {

            var result = await _userService.RegisterAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            try
            {
                var result = await _userService.LoginAsync(user);
                return Ok(new { Token = result, Message = $"Autentificat ca {user.username}" });
            }catch(LockedOutException e)
            {
                return BadRequest(e.Message);
            }catch(WrongDetailsException e)
            {
                return NotFound(e.Message);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
