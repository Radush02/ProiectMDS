using Microsoft.EntityFrameworkCore;
using ProiectMDS.Data;
using ProiectMDS.Models;

namespace ProiectMDS.Models.Repositories.CardRepositories
{
    public class CardRepository : ICardRepository
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

        public async Task DeleteCard(int id)
        {
            var k = await _dbcontext.Card.Where(x => x.CardId == id).FirstOrDefaultAsync();
            if (k != null)
                _dbcontext.Card.Remove(k);

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<Card> CardById(int id)
        {
            var c = await _dbcontext.Card.FirstOrDefaultAsync(i => i.CardId == id);
            return c;
        }

        public async Task UpdateCard(Card card)
        {
            _dbcontext.Card.Update(card);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
