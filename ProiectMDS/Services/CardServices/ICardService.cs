using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services.CardServices
{
    public interface ICardService
    {
        Task AddCard(CardDTO cardDTO, string username);
        Task DeleteCard(int id);
    }
}
