using Microsoft.AspNetCore.Mvc;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Services.ChirieServices;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChirieController : ControllerBase
    {
        private readonly IChirieService _chirieService;

        public ChirieController(IChirieService chirieService)
        {
            _chirieService = chirieService;
        }

        [HttpPost]
        public async Task<IActionResult> AddChirie(ChirieDTO chirieDTO)
        {
            await _chirieService.AddChirie(chirieDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChirie(int id)
        {
            try
            {
                await _chirieService.DeleteChirie(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
