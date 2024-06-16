using Microsoft.EntityFrameworkCore;
using ProiectMDS.Data;
using ProiectMDS.Migrations;
using ProiectMDS.Models;

namespace ProiectMDS.Repositories.SupportRepositories
{
    public class SupportRepository : ISupportRepository
    {
        private readonly ProjectDbContext _dbcontext;
        public SupportRepository(ProjectDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task AddSupport(Support support)
        {
            await _dbcontext.Support.AddAsync(support);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Support>> getAllSupports()
        {
            return await _dbcontext.Support.ToListAsync();
        }
        public async Task<int> getMaxID()
        {
            try
            {
                int max = await _dbcontext.Support.MaxAsync(x => x.SupportId);
                return max;
            }catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<IEnumerable<Support>> getSupportByUserId(int userId)
        {
            return await _dbcontext.Support.Where(s => s.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Support>> getSupportBySupportId(int supportId)
        {
            return await _dbcontext.Support.Where(s => s.SupportId == supportId).ToListAsync();
        }

        public async Task<User> UserById(int userId)
        {
            var user = await _dbcontext.User.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception($"Nu exista user cu id-ul {userId}");
            }
            return user;
        }
    }
}
