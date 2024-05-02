using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ProiectMDS.Models;
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
        public async Task<IActionResult> AddChirie(ChirieDTO chirieDTO, int postareId, int userId)
        {
            await _chirieService.AddChirie(chirieDTO, postareId, userId);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChirie([FromBody] ChirieDTO chirie, int id)
        {
            await _chirieService.UpdateChirie(chirie, id);
            return Ok();
        }

        [HttpGet("dataStart/{dataStart}")]
        public async Task<IActionResult> ChirieByDataStart(DateTime dataStart)
        {
            await _chirieService.ChirieByDataStart(dataStart);
            return Ok();
        }

        [HttpGet("dataStop/{dataStop}")]
        public async Task<IActionResult> ChirieByDataStop(DateTime dataStop)
        {
            await _chirieService.ChirieByDataStop(dataStop);
            return Ok();
        }

        [HttpGet("data/{dataStart}/{dataStop}")]
        public async Task<IActionResult> ChirieByData(DateTime dataStart, DateTime dataStop)
        {
            await _chirieService.ChirieByData(dataStart, dataStop);
            return Ok();
        }
    }
}
