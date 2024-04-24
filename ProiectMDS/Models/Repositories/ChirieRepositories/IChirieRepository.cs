using ProiectMDS.Models;

namespace ProiectMDS.Models.Repositories.ChirieRepositories
{
    public interface IChirieRepository
    {
        Task AddChirie(Chirie chirie);
        Task DeleteChirie(int id);
        Task<Chirie> ChirieById(int id);
        Task UpdateChirie(Chirie c);
    }
}
