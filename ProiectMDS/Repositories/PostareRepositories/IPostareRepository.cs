using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Repositories
{
    public interface IPostareRepository
    {
        Task AddPostare(Postare postare);
        Task DeletePostare(int id);
        Task<Postare> PostareById(int postareId);
        Task<IEnumerable<Postare>> getPostare();
        Task UpdatePostare(Postare postare);
        Task<IEnumerable<PostareDTO>> PostareByTitlu(string titlu);
        Task<IEnumerable<PostareDTO>> PostareByPret(int pretMinim, int pretMaxim);
        Task<IEnumerable<PostareDTO>> PostareByKm(int kmMinim, int kmMaxim);
        Task<IEnumerable<PostareDTO>> PostareByAn(int anMinim, int anMaxim);
        Task<IEnumerable<PostareDTO>> PostareByFirma(string firma);
        Task<IEnumerable<PostareDTO>> PostareByModel(string model);
    }
}
