using ProiectMDS.Models;

namespace ProiectMDS.Models.Repositories.CardRepositories
{
    public interface ICardRepository
    {
        Task AddCard(Card card);
        Task DeleteCard(int id);
    }
}
