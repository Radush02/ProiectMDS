using Microsoft.AspNetCore.Mvc;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Services.CardServices;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService cardService;

        public CardController(ICardService cardService)
        {
            this.cardService = cardService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(CardDTO cardDTO, string username)
        {
            await cardService.AddCard(cardDTO, username);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            try
            {
                await cardService.DeleteCard(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
