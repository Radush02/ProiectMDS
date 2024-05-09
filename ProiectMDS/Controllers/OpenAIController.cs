using Microsoft.AspNetCore.Mvc;
using ProiectMDS.Models;
using ProiectMDS.Services;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;
        public OpenAIController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpPost("getdescription")]
        public async Task<IActionResult> GetDescription([FromBody] OpenAIDTO prompt)
        {
            var result = await _openAIService.GetDescription(prompt);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
