using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface ICardService
    {
        Task AddCard(CardDTO cardDTO, int userId);
        Task DeleteCard(int id);
        Task UpdateCard(CardDTO card, int id);
    }
}
