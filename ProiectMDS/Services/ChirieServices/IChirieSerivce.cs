using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface IChirieService
    {
        Task AddChirie(ChirieDTO chirieDTO, int postareId, int userId);
        Task DeleteChirie(int id);
        Task UpdateChirie(ChirieDTO chirieDTO, int id);
    }
}
