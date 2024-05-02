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
        public async Task<IEnumerable<Postare>> getPostare()
        {
            return await _dbContext.Postare.ToListAsync();
        }
        public async Task UpdatePostare(Postare p)
        {
            _dbContext.Postare.Update(p);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostareDTO>> PostareByAn(int anMinim, int anMaxim)
        {
            var p = await _dbContext.Postare.Where(pr => pr.anFabricatie >= anMinim && pr.anFabricatie <= anMaxim).ToListAsync();

            var postareDTOs = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare

            });

            return postareDTOs;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByFirma(string firma)
        {
            var p = await _dbContext.Postare.Where(pr => pr.firma.ToLower().Contains(firma.ToLower())).ToListAsync();

            var postareDTOs = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare

            });

            return postareDTOs;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByKm(int kmMinim, int kmMaxim)
        {
            var p = await _dbContext.Postare.Where(pr => pr.kilometraj >= kmMinim && pr.kilometraj <= kmMaxim).ToListAsync();

            var postareDTOs = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare

            });

            return postareDTOs;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByModel(string model)
        {
            var p = await _dbContext.Postare.Where(pr => pr.model.ToLower().Contains(model.ToLower())).ToListAsync();

            var postareDTOs = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare

            });

            return postareDTOs;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByPret(int pretMinim, int pretMaxim)
        {
            var p = await _dbContext.Postare.Where(pr => pr.pret >= pretMinim && pr.pret <= pretMaxim).ToListAsync();

            var postareDTOs = p.Select(po => new PostareDTO
            {
                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare

            });

            return postareDTOs;
        }

        public async Task<IEnumerable<PostareDTO>> PostareByTitlu(string titlu)
        {
            var p = await _dbContext.Postare.Where(pr => pr.titlu.ToLower().Contains(titlu.ToLower())).ToListAsync();

            var postareDTOs = p.Select(po => new PostareDTO
            {

                userId = po.UserId,
                titlu = po.titlu,
                descriere = po.descriere,
                pret = po.pret,
                firma = po.firma,
                model = po.model,
                kilometraj = po.kilometraj,
                anFabricatie = po.anFabricatie,
                talon = po.talon,
                carteIdentitateMasina = po.carteIdentitateMasina,
                asigurare = po.asigurare

            });

            return postareDTOs;
        }
    }
}

