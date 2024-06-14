using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services.ChirieServices
{
    public interface IChirieService
    {
        Task AddChirie(ChirieDTO chirieDTO, int postareId, int userId);
        Task DeleteChirie(int id);
        Task UpdateChirie(ChirieDTO chirieDTO, int id);
        Task<IEnumerable<ChirieDTO>> ChirieByDataStart(DateTime dataStart);
        Task<IEnumerable<ChirieDTO>> ChirieByDataStop(DateTime dataStop);
        Task<IEnumerable<ChirieDTO>> ChirieByData(DateTime dataStart, DateTime dataStop);
        Task rentConfirmationEmail(ChirieDTO chirie);
    }
}
