using Microsoft.EntityFrameworkCore;
using ProiectMDS.Data;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;
using System.Linq;

namespace ProiectMDS.Repositories
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
        public async Task<int> CountPostare()
        {
            return await _dbContext.Postare.CountAsync();
        }
        public async Task<IEnumerable<Postare>> getPostare()
        {
            return await _dbContext.Postare.ToListAsync();
        }
        public async Task UpdatePostare(Postare p)
        {
            _dbContext.Postare.Update(p);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Postare>> PostareByAn(int anMinim, int anMaxim)
        {
            var p = await _dbContext.Postare.Where(pr => pr.anFabricatie >= anMinim && pr.anFabricatie <= anMaxim).ToListAsync();

            return p;
        }

        public async Task<IEnumerable<Postare>> PostareByFirma(string firma)
        {
            var p = await _dbContext.Postare.Where(pr => pr.firma.ToLower().Contains(firma.ToLower())).ToListAsync();

            return p;
        }

        public async Task<IEnumerable<Postare>> PostareByKm(int kmMinim, int kmMaxim)
        {
            var p = await _dbContext.Postare.Where(pr => pr.kilometraj >= kmMinim && pr.kilometraj <= kmMaxim).ToListAsync();

            return p;
        }

        public async Task<IEnumerable<Postare>> PostareByModel(string model)
        {
            var p = await _dbContext.Postare.Where(pr => pr.model.ToLower().Contains(model.ToLower())).ToListAsync();

            return p;
        }

        public async Task<IEnumerable<Postare>> PostareByPret(int pretMinim, int pretMaxim)
        {
            var p = await _dbContext.Postare.Where(pr => pr.pret >= pretMinim && pr.pret <= pretMaxim).ToListAsync();

            return p;
        }

        public async Task<IEnumerable<Postare>> PostareByTitlu(string titlu)
        {
            var p = await _dbContext.Postare.Where(pr => pr.titlu.ToLower().Contains(titlu.ToLower())).ToListAsync();

            return p;
        }
    }
}

