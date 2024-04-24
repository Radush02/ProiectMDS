using ProiectMDS.Models.DTOs;
using ProiectMDS.Models;
using ProiectMDS.Models.Repositories.ReviewRepositories;
using ProiectMDS.Exceptions;

namespace ProiectMDS.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task AddReview(ReviewDTO reviewDTO, int postareId, int userId)
        {
            var review = new Review()
            {
                PostareId = postareId,
                UserId = userId,
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

        public async Task UpdateReview(ReviewDTO reviewDTO, int reviewId)
        {
            var r = await _reviewRepository.ReviewById(reviewId);

            if (r == null)
            {
                throw new NotFoundException($"Nu exista review cu id-ul {reviewId}.");
            }

            r.titlu = reviewDTO.titlu;
            r.comentariu = reviewDTO.comentariu;
            r.rating = reviewDTO.rating;

            await _reviewRepository.UpdateReview(r);
        }
    }
}
