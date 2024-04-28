using ProiectMDS.Models;

namespace ProiectMDS.Models.Repositories.CardRepositories
{
    public interface ICardRepository
    {
        Task AddCard(Card card);
        Task DeleteCard(int id);
        Task<Card> CardById(int id);
        Task UpdateCard(Card c);
    }
}
