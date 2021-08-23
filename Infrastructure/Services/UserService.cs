using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;

        public UserService(IUserRepository userRepository,
            IMovieRepository movieRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;

        }

        public async Task<UserLoginResponseModel> GetById(int id) {
            var user = await _userRepository.GetByIdAsync(id);
            var userResponseModel = new UserLoginResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            return userResponseModel;
        }
        public async Task<UserLoginResponseModel> Login(UserLoginRequestModel requestModel)
        {
            var user = await _userRepository.GetUserByEmail(requestModel.Email);
            if (user == null)
            {
                return null;
            }

            var hashedPassword = GetHashedPassword(requestModel.Password, user.Salt);
            if (hashedPassword != user.HashedPassword) return null;
            var response = new UserLoginResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.Email,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            return response;
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
            {
                throw new ConflictException("Email already exists");
            }

            var salt = CreateSalt();
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);

            var user = new User
            {
                Email = requestModel.Email,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Salt = salt,
                HashedPassword = hashedPassword
            };
            var createdUser = await _userRepository.AddAsync(user);

            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            return response;
        }


        public async Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMovies(int userId)
        {
            var user = await _userRepository.GetUserPurchaseById(userId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in user.Purchases)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }
            return movieCards;
        }


        public async Task<IEnumerable<MovieCardResponseModel>> GetFavorites(int userId)
        {
            var user = await _userRepository.GetUserFavoriteById(userId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in user.Favorites)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }
            return movieCards;
        }



        public string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        public string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

    }
    }