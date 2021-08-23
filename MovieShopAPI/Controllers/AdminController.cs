using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    //Attribute Routing
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IPurchaseService _purchaseService;

        public AdminController(IMovieService movieService, IPurchaseService purchaseService)
        {
            _movieService = movieService;
            _purchaseService = purchaseService;
        }

        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> PostMovie([FromBody] MovieCardRequestModel model)
        {
            var postmovie = await _movieService.PostMovie(model);
            return Ok(postmovie);

        }

        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> PutMovie([FromBody] MovieCardRequestModel model)
        {
            var putmovie = await _movieService.PutMovie(model);
            return Ok(putmovie);

        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetPurchases()
        {
            var getpurchase = await _purchaseService.GetPurchase();
            return Ok(getpurchase);
        }
    }
}