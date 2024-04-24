using ProiectMDS.Models;

namespace ProiectMDS.Models.Repositories.ReviewRepositories
{
    public interface IReviewRepository
    {
        Task AddReview(Review review);
        Task DeleteReview(int id);
        Task<Review> ReviewById(int id);
        Task UpdateReview(Review review);
    }
}
