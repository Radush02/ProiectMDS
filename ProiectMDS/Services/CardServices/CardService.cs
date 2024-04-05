using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Repositories.CardRepositories;

namespace ProiectMDS.Services.CardServices
{
    public class CardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task AddCard(CardDTO cardDTO, string username)
        {
            //var user =
            var card = new Card()
            {
                numar = cardDTO.numar,
                dataExpirare = cardDTO.dataExpirare,
                nume = cardDTO.nume,
                cvv = cardDTO.cvv,
                UserId = cardDTO.UserId
            };
            await _cardRepository.AddCard(card);
        }

        public async Task DeleteCard(int id)
        {
            await _cardRepository.DeleteCard(id);
        }
    }
}
