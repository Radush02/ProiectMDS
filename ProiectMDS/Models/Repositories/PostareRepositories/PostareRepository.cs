using Microsoft.EntityFrameworkCore;
using ProiectMDS.Data;
using ProiectMDS.Models;

namespace ProiectMDS.Models.Repositories.PostareRepositories
{
    public class PostareRepository : IPostareRepository
    {
        private readonly ProjectDbContext _dbContext;

        public PostareRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPostare(Postare postare)
        {
            await _dbContext.Postare.AddAsync(postare);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePostare(int id)
        {
            var k = await _dbContext.Postare.Where(x => x.PostareId == id).FirstOrDefaultAsync();
            if (k != null)
                _dbContext.Postare.Remove(k);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Postare> PostareById(int id)
        {
            var p = await _dbContext.Postare.FirstOrDefaultAsync(i => i.PostareId == id);
            return p;
        }

        public async Task UpdatePostare(Postare p)
        {
            _dbContext.Postare.Update(p);
            await _dbContext.SaveChangesAsync();
        }
    }
}

