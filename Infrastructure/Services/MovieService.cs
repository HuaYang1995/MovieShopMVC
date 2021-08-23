using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        public async Task<List<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.Get30TopRatedMovies();
            var movieCards = new List<MovieCardResponseModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }

            return movieCards;

        }

        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.Get30HighestRevenueMovies();

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }
            return movieCards;
        }


        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {

            var movie = await _movieRepository.GetByIdAsync(id);

            var casts = new List<CastResponseModel>();
            foreach (var ca in movie.MovieCasts)
            {
                casts.Add(new CastResponseModel
                {
                    Id = ca.CastId,
                    Name = ca.Cast.Name,
                    Gender = ca.Cast.Gender,
                    TmdbUrl = ca.Cast.TmdbUrl,
                    ProfilePath = ca.Cast.ProfilePath,
                    Character = ca.Character
                });
            }

            var genres = new List<GenreResponseModel>();
            foreach (var gen in movie.MovieGenres)
            {
                genres.Add(new GenreResponseModel
                {

                    Id = gen.Genre.Id,
                    Name = gen.Genre.Name
                });
            }

            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Budget = movie.Budget,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                ImdbUrl = movie.ImdbUrl,
                Revenue = movie.Revenue,
                TmdbUrl = movie.TmdbUrl,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                OriginalLanguage = movie.OriginalLanguage,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
                Casts = casts,
                Genres = genres,
                Rating = movie.Rating

            };
            return movieDetails;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId);
            return movies;
        }

        public async Task<Movie> PostMovie(MovieCardRequestModel model)
        {
            var movie = new Movie
            {
                Title = model.Title,
                Overview = model.Overview,
                Tagline = model.Tagline,
                Budget = model.Budget,
                ImdbUrl = model.ImdbUrl,
                TmdbUrl = model.TmdbUrl,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                OriginalLanguage = model.OriginalLanguage,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Price = model.Price,
                CreatedDate = model.CreatedDate
            };

            var movies = await _movieRepository.AddAsync(movie);
            return movies;
        }

        public async Task<Movie> PutMovie(MovieCardRequestModel model)
        {
            var movie = new Movie
            {
                Title = model.Title,
                Overview = model.Overview,
                Tagline = model.Tagline,
                Budget = model.Budget,
                ImdbUrl = model.ImdbUrl,
                TmdbUrl = model.TmdbUrl,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                OriginalLanguage = model.OriginalLanguage,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Price = model.Price,
                CreatedDate = model.CreatedDate
            };

            var movies = await _movieRepository.UpdateAsync(movie);
            return movies;


        }
    }
}
