using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Models.Repositories.PostareRepositories
{
    public interface IPostareRepository
    {
        Task AddPostare(Postare postare);
        Task DeletePostare(int id);
        Task<Postare> PostareById(int postareId);
        Task UpdatePostare(Postare postare);
        Task<IEnumerable<PostareDTO>> PostareByTitlu(String titlu);
        Task<IEnumerable<PostareDTO>> PostareByPret(int pretMinim, int pretMaxim);
        Task<IEnumerable<PostareDTO>> PostareByKm(int kmMinim, int kmMaxim);
        Task<IEnumerable<PostareDTO>> PostareByAn(int anMinim, int anMaxim);
        Task<IEnumerable<PostareDTO>> PostareByFirma(String firma);
        Task<IEnumerable<PostareDTO>> PostareByModel(String model);
    }
}
