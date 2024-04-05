using ProiectMDS.Data;
using ProiectMDS.Models;

namespace ProiectMDS.Repositories
{
    public class CardRepository
    {
        private readonly ProjectDbContext _dbcontext;
        public CardRepository(ProjectDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task AddCard(Card card)
        {
            await _dbcontext.Card.AddAsync(card);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
