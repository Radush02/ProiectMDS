using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface IReviewService
    {
        Task AddReview(ReviewDTO reviewDTO, int postareId, int userId);
        Task DeleteReview(int id);
        Task UpdateReview(ReviewDTO reviewDTO, int id);
    }
}
