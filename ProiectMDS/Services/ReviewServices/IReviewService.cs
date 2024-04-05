using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services.ReviewServices
{
    public interface IReviewService
    {
        Task AddReview(ReviewDTO reviewDTO);
        Task DeleteReview(int id);
    }
}
