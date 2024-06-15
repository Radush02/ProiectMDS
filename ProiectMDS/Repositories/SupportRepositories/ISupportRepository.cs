using ProiectMDS.Models;

namespace ProiectMDS.Repositories.SupportRepositories
{
    public interface ISupportRepository
    {
        Task AddSupport(Support support);
        Task<IEnumerable<Support>> getAllSupports();
        Task<IEnumerable<Support>> getSupportByUserId(int userId);
        Task<IEnumerable<Support>> getSupportBySupportId(int supportId);
        Task<User> UserById(int userId); 
    }
}
