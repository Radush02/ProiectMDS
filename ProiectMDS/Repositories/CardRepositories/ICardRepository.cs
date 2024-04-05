using ProiectMDS.Models;

namespace ProiectMDS.Repositories.CardRepositories
{
    public interface ICardRepository
    {
        Task AddCard(Card card);
        Task DeleteCard(int id);
    }
}
