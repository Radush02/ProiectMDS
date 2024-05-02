using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Repositories
{
    public interface IReviewRepository
    {
        Task AddReview(Review review);
        Task DeleteReview(int id);
        Task<Review> ReviewById(int id);
        Task UpdateReview(Review review);
        Task<IEnumerable<ReviewDTO>> ReviewByRating(int rating);
    }
}
