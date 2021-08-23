using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllGenres();
            return View(genres);
        }
        public async Task<IActionResult> Details(int id)
        {
            var genre = await _genreService.GetGenreById(id);
            ViewData["genreName"] = genre.Name;
            return View(genre);
        }
        public PartialViewResult ShowInNavBar()
        {
            var genres = _genreService.GetAllGenres();
            return PartialView("~/Views/Shared/_Layout.cshtml", genres);
        }
    }
}