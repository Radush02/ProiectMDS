using Microsoft.EntityFrameworkCore;
using ProiectMDS.Data;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Repositories
{
    public class ChirieRepository : IChirieRepository
    {
        private readonly ProjectDbContext _dbContext;

        public ChirieRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> UserById(int userId)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }

        public async Task UpdatePuncteFidelitate(User user)
        {
            _dbContext.User.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddChirie(Chirie chirie)
        {
            var c = await _dbContext.Chirie.Where(x => x.PostareId == chirie.PostareId && (x.dataStart <= chirie.dataStart && (x.dataStop >= chirie.dataStop || x.dataStop >= chirie.dataStart) || x.dataStart >= chirie.dataStart && (x.dataStart <= chirie.dataStop || x.dataStop <= chirie.dataStop))).FirstOrDefaultAsync();
            if (c != null)
            {
                throw new Exception("Masina este deja inchiriata in aceasta perioada");
            }
            await _dbContext.Chirie.AddAsync(chirie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteChirie(int id)
        {
            var k = await _dbContext.Chirie.Where(x => x.ChirieId == id).FirstOrDefaultAsync();
            if (k != null)
                _dbContext.Chirie.Remove(k);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Chirie> ChirieById(int id)
        {
            var c = await _dbContext.Chirie.FirstOrDefaultAsync(i => i.ChirieId == id);
            return c;
        }

        public async Task UpdateChirie(Chirie c)
        {
            _dbContext.Chirie.Update(c);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chirie>> ChirieByDataStart(DateTime dataStart)
        {
            var c = await _dbContext.Chirie.Where(ch => ch.dataStart == dataStart).ToListAsync();

            return c;
        }

        public async Task<IEnumerable<Chirie>> ChirieByDataStop(DateTime dataStop)
        {
            var c = await _dbContext.Chirie.Where(ch => ch.dataStop == dataStop).ToListAsync();

            return c;
        }

        public async Task<IEnumerable<Chirie>> ChirieByData(DateTime dataStart, DateTime dataStop)
        {
            var c = await _dbContext.Chirie.Where(ch => ch.dataStop == dataStop && ch.dataStart == dataStart).ToListAsync();

            return c;
        }

        public async Task<int> UserByPostareId(int postareId)
        {
            var p = await _dbContext.Postare.FirstOrDefaultAsync(i => i.PostareId == postareId);
            return p.UserId;
        }
    }
}
