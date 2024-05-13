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
        Task<int> CountPostare();
        Task<int> NrPostareByUser(int userId);
        Task UpdatePostare(Postare postare);
        Task<IEnumerable<Postare>> PostareByTitlu(string titlu);
        Task<IEnumerable<Postare>> PostareByPret(int pretMinim, int pretMaxim);
        Task<IEnumerable<Postare>> PostareByKm(int kmMinim, int kmMaxim);
        Task<IEnumerable<Postare>> PostareByAn(int anMinim, int anMaxim);
        Task<IEnumerable<Postare>> PostareByFirma(string firma);
        Task<IEnumerable<Postare>> PostareByModel(string model);
    }
}
