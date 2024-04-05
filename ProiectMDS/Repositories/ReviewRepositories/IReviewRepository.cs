using ProiectMDS.Models;

namespace ProiectMDS.Repositories.ReviewRepositories
{
    public interface IReviewRepository
    {
        Task AddReview(Review review);
        Task DeleteReview(int id);
    }
}
