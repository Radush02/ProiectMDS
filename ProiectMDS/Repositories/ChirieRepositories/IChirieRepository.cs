using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Repositories
{
    public interface IChirieRepository
    {
        Task AddChirie(Chirie chirie);
        Task DeleteChirie(int id);
        Task<Chirie> ChirieById(int id);
        Task UpdateChirie(Chirie c);
        Task<IEnumerable<Chirie>> ChirieByDataStart(DateTime dataStart);
        Task<IEnumerable<Chirie>> ChirieByDataStop(DateTime dataStop);
        Task<IEnumerable<Chirie>> ChirieByData(DateTime dataStart, DateTime dataStop);
        Task<User> UserById(int id);
        Task UpdatePuncteFidelitate(User user);
        Task<int> UserByPostareId(int postareId);
        Task<Postare> PostareById(int id);
    }
}
