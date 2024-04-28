using Microsoft.EntityFrameworkCore;
using ProiectMDS.Data;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Models.Repositories.ChirieRepositories
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

        public async Task<IEnumerable<ChirieDTO>> ChirieByDataStart(DateTime dataStart)
        {
            var c = await _dbContext.Chirie.Where(ch => ch.dataStart == dataStart).ToListAsync();

            var chirieDTOs = c.Select(ch => new ChirieDTO
            {
                dataStart = ch.dataStart,
                dataStop = ch.dataStop

            });

            return chirieDTOs;
        }

        public async Task<IEnumerable<ChirieDTO>> ChirieByDataStop(DateTime dataStop)
        {
            var c = await _dbContext.Chirie.Where(ch => ch.dataStop == dataStop).ToListAsync();

            var chirieDTOs = c.Select(ch => new ChirieDTO
            {
                dataStart = ch.dataStart,
                dataStop = ch.dataStop

            });

            return chirieDTOs;
        }

        public async Task<IEnumerable<ChirieDTO>> ChirieByData(DateTime dataStart, DateTime dataStop)
        {
            var c = await _dbContext.Chirie.Where(ch => ch.dataStop == dataStop && ch.dataStart == dataStart).ToListAsync();

            var chirieDTOs = c.Select(ch => new ChirieDTO
            {
                dataStart = ch.dataStart,
                dataStop = ch.dataStop

            });

            return chirieDTOs;
        }
    }
}
