using Microsoft.EntityFrameworkCore;
using ProiectMDS.Data;
using ProiectMDS.Models;

namespace ProiectMDS.Repositories.ReviewRepositories
{
    public class ReviewRepository
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
    }
}
