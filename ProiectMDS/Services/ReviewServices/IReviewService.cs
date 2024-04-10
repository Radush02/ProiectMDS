using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface IReviewService
    {
        Task AddReview(ReviewDTO reviewDTO);
        Task DeleteReview(int id);
    }
}
