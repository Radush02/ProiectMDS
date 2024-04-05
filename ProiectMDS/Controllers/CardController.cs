using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddCard(CardDTO cardDTO, string username)
        {
            await cardService.AddCard(cardDTO, username);
            return Ok();
        }
    }
}
