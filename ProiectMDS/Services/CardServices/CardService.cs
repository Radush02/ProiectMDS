using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ProiectMDS.Exceptions;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;
using ProiectMDS.Repositories;

namespace ProiectMDS.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task AddCard(CardDTO cardDTO)
        {
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

        public async Task UpdateCard(CardDTO card, int id)
        {
            var c = await _cardRepository.CardById(id);

            if (c == null)
            {
                throw new NotFoundException($"Nu exista card cu id-ul {id}.");
            }

            c.numar = card.numar;
            c.dataExpirare = card.dataExpirare;
            c.nume = card.nume;
            c.cvv = card.cvv;

            await _cardRepository.UpdateCard(c);
        }
        public async Task<IEnumerable<CardDTO>> CardByUserID(int id)
        {
            var cards = await _cardRepository.CardByUserID(id);
            return cards.Select(c => new CardDTO()
            {
                numar = c.numar,
                dataExpirare = c.dataExpirare,
                nume = c.nume,
                cvv = c.cvv,
                UserId = c.UserId
            });
        }
    }
}
