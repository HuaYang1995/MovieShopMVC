using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopAPI.Controllers
{
    //Attribute Routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;

        public MoviesController(IMovieService movieService, IReviewService reviewService)
        {
            _movieService = movieService;
            _reviewService = reviewService;
        }
        [Route("toprated")]
        [HttpGet]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies_rated = await _movieService.GetTopRatedMovies();
            if (!movies_rated.Any())
            {
                return NotFound("No Movies Found");
            }
            return Ok(movies_rated);
        }


        [Route("toprevenue")]
        [HttpGet]
        public async Task<IActionResult> GetTopRenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();
            if (!movies.Any())
            {

                return NotFound("No Movies Found");

            };

            return Ok(movies);
            //Serialzation => object to another type of object
            //c# to JSON
            //DeSerialization => JSON to C#
            // .NET Core 3.1 or less = JSON.NET=> 3RD party libarary, included
            //System.Text.Json=>
            // along with data you also need to return HTTP status code

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var moviesById = await _movieService.GetMovieDetails(id);
            if (moviesById == null)
            {

                return NotFound("No Movies Found");

            };

            return Ok(moviesById);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenre(genreId);
            return Ok(movies);
        }

        [HttpGet]
        [Route("{Id:int}/review")]
        public async Task<IActionResult> GetReviewById(int Id)
        {
            var movies = await _reviewService.GetAllReviewsById(Id);
            return Ok(movies);
        }



    }
}
