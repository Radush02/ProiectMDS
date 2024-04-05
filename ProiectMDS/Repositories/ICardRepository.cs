using ProiectMDS.Models;

namespace ProiectMDS.Repositories
{
    public interface ICardRepository
    {
        Task AddCard(Card card);
    }
}
