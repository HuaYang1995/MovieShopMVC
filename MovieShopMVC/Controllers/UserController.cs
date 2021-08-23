using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;

        public UserController(ICurrentUserService currentUserService, IUserService userService, IMovieService movieService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
            _movieService = movieService;
        }


        public async Task<IActionResult> GetAllPurchases()
        {
            var userId = _currentUserService.UserId;
            // id from the cookie and sent that id to UserService to get all his/her movies.
            // Filters
            var movieCards = await _userService.GetPurchasedMovies(userId);
            // call userservice GetAll Purchases
            return View(movieCards);
        }


        public async Task<IActionResult> GetFavorites()
        {
            
            var userId = _currentUserService.UserId;
            var movieCards = await _userService.GetFavorites(userId);
            return View(movieCards);
        }


        public async Task<IActionResult> GetProfile()
        {
            var userId = _currentUserService.UserId;
            return View();
        }


        public async Task<IActionResult> EditProfile()
        {
            var userId = _currentUserService.UserId;
            return View();
        }


        public async Task<IActionResult> BuyMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(3);
            return View(movie);
        }


        public async Task<IActionResult> AddFavoriteMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(9);
            return View(movie);
        }
    }
}