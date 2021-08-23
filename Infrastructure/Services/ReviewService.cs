using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMovieService _movieService;
        public ReviewService(IReviewRepository reviewRepository, ICurrentUserService currentUserService, IMovieService movieService)
        {
            _reviewRepository = reviewRepository;
            _currentUserService = currentUserService;
            _movieService = movieService;

        }
        public async Task<IEnumerable<Review>> GetAllReviewsById(int id)
        {
            var movie = await _reviewRepository.GetReviewByMovieId(id);
            return movie;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByUserId(int id)
        {
            var movie = await _reviewRepository.GetReviewByUserId(id);
            return movie;
        }

        public async Task<Review> PostReview(ReviewRequestModel model)
        {
            var userId = _currentUserService.UserId;
            var movie = await _movieService.GetMovieDetails(model.MovieId);

            var review = new Review {
                UserId = userId,
                MovieId = movie.Id,
                ReviewText = model.ReviewText,
                Rating = model.Rating
            };
            var reviews = await _reviewRepository.AddAsync(review);
            return reviews;
        }

        public async Task<Review> PutReview(ReviewRequestModel model)
        {
            var userId = _currentUserService.UserId;
            var movie = await _movieService.GetMovieDetails(model.MovieId);

            var review = new Review
            {
                UserId = userId,
                MovieId = movie.Id,
                ReviewText = model.ReviewText,
                Rating = model.Rating
            };
            var reviews = await _reviewRepository.UpdateAsync(review);
            return reviews;
        }
    }
}
