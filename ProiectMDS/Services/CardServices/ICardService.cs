using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface ICardService
    {
        Task AddCard(CardDTO cardDTO);
        Task DeleteCard(int id);
        Task UpdateCard(CardDTO card, int id);
        Task<IEnumerable<CardDTO>> CardByUserID(int id);


    }
}
