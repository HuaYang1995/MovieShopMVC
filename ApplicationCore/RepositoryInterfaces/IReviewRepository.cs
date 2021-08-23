using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IReviewRepository : IAsyncRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewByMovieId(int MovieId);
        Task<IEnumerable<Review>> GetReviewByUserId(int UserId);
    }
}
