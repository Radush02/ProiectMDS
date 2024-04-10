using ProiectMDS.Models;

namespace ProiectMDS.Models.Repositories.ReviewRepositories
{
    public interface IReviewRepository
    {
        Task AddReview(Review review);
        Task DeleteReview(int id);
    }
}
