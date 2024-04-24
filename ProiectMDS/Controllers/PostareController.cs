using Microsoft.AspNetCore.Mvc;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Services;

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
        public async Task<IActionResult> AddPostare(PostareDTO postareDTO, int userId)
        {
            await _postareService.AddPostare(postareDTO, userId);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostare([FromBody] PostareDTO postare, int id)
        {
            await _postareService.UpdatePostare(postare, id);
            return Ok();
        }
    }
}
