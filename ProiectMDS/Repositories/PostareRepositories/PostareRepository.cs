using ProiectMDS.Data;
using ProiectMDS.Models;

namespace ProiectMDS.Repositories.PostareRepositories
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
            var postare = await _dbContext.Postare.FindAsync(id);
            if (postare != null)
            {
                _dbContext.Postare.Remove(postare);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

