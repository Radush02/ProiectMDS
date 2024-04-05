using Microsoft.AspNetCore.Mvc;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Services.PostareServices;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostareController : ControllerBase
    {
        private readonly IPostareService _postareService;

        public PostareController(IPostareService postareService)
        {
            _postareService = postareService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPostare(PostareDTO postareDTO)
        {
            await _postareService.AddPostare(postareDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostare(int id)
        {
            try
            {
                await _postareService.DeletePostare(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
