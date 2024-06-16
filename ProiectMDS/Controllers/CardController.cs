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
        [HttpGet("{id}")]
        public async Task<IActionResult> CardByUserID(int id)
        {
            var cards = await cardService.CardByUserID(id);
            return Ok(cards);
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(CardDTO cardDTO)
        {
            await cardService.AddCard(cardDTO);
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
