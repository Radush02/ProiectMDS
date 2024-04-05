using ProiectMDS.Models;

namespace ProiectMDS.Repositories.ChirieRepositories
{
    public interface IChirieRepository
    {
        Task AddChirie(Chirie chirie);
        Task DeleteChirie(int id);
    }
}
