using Microsoft.EntityFrameworkCore;
using ProiectMDS.Data;
using ProiectMDS.Models;
using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ProjectDbContext _dbcontext;
        public ReviewRepository(ProjectDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task AddReview(Review review)
        {
            await _dbcontext.Review.AddAsync(review);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteReview(int id)
        {
            var k = await _dbcontext.Review.Where(x => x.ReviewId == id).FirstOrDefaultAsync();
            if (k != null)
                _dbcontext.Review.Remove(k);

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<Review> ReviewById(int id)
        {
            var r = await _dbcontext.Review.FirstOrDefaultAsync(i => i.ReviewId == id);
            return r;
        }

        public async Task UpdateReview(Review r)
        {
            _dbcontext.Review.Update(r);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewDTO>> ReviewByRating(int rating)
        {
            var r = await _dbcontext.Review.Where(rr => rr.rating == rating).ToListAsync();

            var reviewDTOs = r.Select(re => new ReviewDTO
            {
                comentariu = re.comentariu,
                rating = re.rating,
                titlu = re.titlu,
                dataReview = re.dataReview

            });

            return reviewDTOs;
        }
    }
}
