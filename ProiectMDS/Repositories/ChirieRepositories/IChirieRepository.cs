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
        Task<IEnumerable<ChirieDTO>> ChirieByDataStart(DateTime dataStart);
        Task<IEnumerable<ChirieDTO>> ChirieByDataStop(DateTime dataStop);
        Task<IEnumerable<ChirieDTO>> ChirieByData(DateTime dataStart, DateTime dataStop);
    }
}
