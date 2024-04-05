using ProiectMDS.Models.DTOs;
using ProiectMDS.Models;
using ProiectMDS.Repositories;
using ProiectMDS.Repositories.ReviewRepositories;

namespace ProiectMDS.Services.ReviewServices
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task AddReview(ReviewDTO reviewDTO)
        {
            var review = new Review()
            {
                PostareId = reviewDTO.PostareId,
                UserId = reviewDTO.UserId,
                titlu = reviewDTO.titlu,
                comentariu = reviewDTO.comentariu,
                rating = reviewDTO.rating,
                dataReview = reviewDTO.dataReview
            };
            await _reviewRepository.AddReview(review);
        }

        public async Task DeleteReview(int id)
        {
            await _reviewRepository.DeleteReview(id);
        }
    }
}
