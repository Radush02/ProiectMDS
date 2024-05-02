using ProiectMDS.Models;

namespace ProiectMDS.Repositories
{
    public interface ICardRepository
    {
        Task AddCard(Card card);
        Task DeleteCard(int id);
        Task<Card> CardById(int id);
        Task UpdateCard(Card c);
    }
}
