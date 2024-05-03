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
        Task<IEnumerable<Review>> ReviewByRating(int rating);
        Task<IEnumerable<Review>> GetReviewByDateAsc();
        Task<IEnumerable<Review>> GetReviewByDateDesc();
        Task<IEnumerable<Review>> GetReviewByRatingAsc();
        Task<IEnumerable<Review>> GetReviewByRatingDesc();
        Task<int> UserByPostareId(int postareId);
    }
}
