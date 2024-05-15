using Microsoft.EntityFrameworkCore;
using ProiectMDS.Data;
using ProiectMDS.Models;
using System.Linq;

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
            var r = await _dbcontext.Review.Where(x => x.PostareId == review.PostareId && x.UserId == review.UserId).FirstOrDefaultAsync();
            if (r != null)
            {
                throw new Exception("Ai dat deja review acestei postari");
            }
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
            if(r == null)
            {
                throw new Exception($"Nu exista review cu id-ul {id}");
            }
            return r;
        }

        public async Task UpdateReview(Review r)
        {
            _dbcontext.Review.Update(r);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> ReviewByRating(int rating)
        {
            var r = await _dbcontext.Review.Where(rr => rr.rating == rating).ToListAsync();

            return r;
        }

        public async Task<IEnumerable<Review>> GetReviewByDateAsc()
        {
            return  await _dbcontext.Review.ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewByDateDesc()
        {
            return await _dbcontext.Review.ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewByRatingDesc()
        {
            return await _dbcontext.Review.ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewByRatingAsc()
        {
            return await _dbcontext.Review.ToListAsync();
        }

        public async Task<int> UserByPostareId(int postareId)
        {
            var p = await _dbcontext.Postare.FirstOrDefaultAsync(i => i.PostareId == postareId);
            if(p == null)
            {
                throw new Exception($"Nu exista postare cu id-ul {postareId}");
            }
            return p.UserId;
        }
    }
}
