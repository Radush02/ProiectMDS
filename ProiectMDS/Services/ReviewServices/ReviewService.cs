using ProiectMDS.Models.DTOs;
using ProiectMDS.Models;
using ProiectMDS.Exceptions;
using ProiectMDS.Repositories;
using System.Linq;

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
            if(userId == await _reviewRepository.UserByPostareId(postareId))
            {
                throw new Exception("Nu poti da review unei postari publicate de tine!");
            }

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

        public async Task<IEnumerable<ReviewDTO>> ReviewByRating(int rating)
        {
            var r = await _reviewRepository.ReviewByRating(rating);

            if (r == null)
            {
                throw new NotFoundException($"Nu exista review cu acest rating {rating}.");
            }

            IEnumerable<ReviewDTO> rez;
            rez = r.Select(re => new ReviewDTO
            {
                titlu = re.titlu,
                comentariu = re.comentariu,
                rating = re.rating,
                dataReview = re.dataReview
            });
            return rez;
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewByDateAsc()
        {
            var r = await _reviewRepository.GetReviewByDateAsc();

            IEnumerable<ReviewDTO> rez;
            rez = r.Select(re => new ReviewDTO
            {
                titlu = re.titlu,
                comentariu = re.comentariu,
                rating = re.rating,
                dataReview = re.dataReview
            });

            IEnumerable<ReviewDTO> rez2 = rez.OrderByDescending(d => d.dataReview);
            return rez2;
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewByDateDesc()
        {
            var r = await _reviewRepository.GetReviewByDateDesc();

            IEnumerable<ReviewDTO> rez;
            rez = r.Select(re => new ReviewDTO
            {
                titlu = re.titlu,
                comentariu = re.comentariu,
                rating = re.rating,
                dataReview = re.dataReview  
            });

            IEnumerable<ReviewDTO> rez2 = rez.OrderByDescending(d => d.dataReview);
            return rez2;
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewByRatingAsc()
        {
            var r = await _reviewRepository.GetReviewByRatingAsc();

            IEnumerable<ReviewDTO> rez;
            rez = r.Select(re => new ReviewDTO
            {
                titlu = re.titlu,
                comentariu = re.comentariu,
                rating = re.rating,
                dataReview = re.dataReview
            });

            IEnumerable<ReviewDTO> rez2 = rez.OrderBy(d => d.rating);
            return rez2;
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewByRatingDesc()
        {
            var r = await _reviewRepository.GetReviewByRatingDesc();

            IEnumerable<ReviewDTO> rez;
            rez = r.Select(re => new ReviewDTO
            {
                titlu = re.titlu,
                comentariu = re.comentariu,
                rating = re.rating,
                dataReview = re.dataReview
            });

            IEnumerable<ReviewDTO> rez2 = rez.OrderByDescending(d => d.rating);
            return rez2;
        }
    }
}
