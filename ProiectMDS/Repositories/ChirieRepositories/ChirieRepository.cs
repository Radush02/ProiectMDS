using ProiectMDS.Data;
using ProiectMDS.Models;

namespace ProiectMDS.Repositories.ChirieRepositories
{
    public class ChirieRepository : IChirieRepository
    {
        private readonly ProjectDbContext _dbContext;

        public ChirieRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddChirie(Chirie chirie)
        {
            await _dbContext.Chirie.AddAsync(chirie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteChirie(int id)
        {
            var chirie = await _dbContext.Chirie.FindAsync(id);
            if (chirie != null)
            {
                _dbContext.Chirie.Remove(chirie);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
