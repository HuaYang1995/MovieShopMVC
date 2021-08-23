using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : EfRepository<Review>,IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Review>> GetReviewByMovieId(int MovieId)
        {
            var movie_review = await _dbContext.Reviews.Where(m => m.MovieId == MovieId).ToListAsync();
            return movie_review;
        }

        public async Task<IEnumerable<Review>> GetReviewByUserId(int UserId)
        {
            var movie_review = await _dbContext.Reviews.Where(m => m.UserId == UserId).ToListAsync();
            return movie_review;
        }

    }
}
