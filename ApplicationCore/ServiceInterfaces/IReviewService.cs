using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsById(int id);
        Task<IEnumerable<Review>> GetAllReviewsByUserId(int id);

        Task<Review> PostReview(ReviewRequestModel model);

        Task<Review> PutReview(ReviewRequestModel model);
    }
}
