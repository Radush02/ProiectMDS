using Microsoft.AspNetCore.Mvc;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Services;

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
        public async Task<IActionResult> AddCard(CardDTO cardDTO, int userId)
        {
            await cardService.AddCard(cardDTO, userId);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard([FromBody] CardDTO card, int id)
        {
            await cardService.UpdateCard(card, id);
            return Ok();
        }

    }
}
