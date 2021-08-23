using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    //Attribute Routing
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;
        private readonly IPurchaseService _purchaseService;

        public UserController(IUserService userService, IReviewService reviewService,
            IPurchaseService purchaseService)
        {
            _userService = userService;
            _reviewService = reviewService;
            _purchaseService = purchaseService;
        }

        [HttpGet]
        [Route("{id:int}/purchase")]

        public async Task<IActionResult> GetPurchaseById(int id)
        {
            var purchaseById = await _userService.GetPurchasedMovies(id);
            return Ok(purchaseById);
        }


        [HttpGet]
        [Route("{id:int}/favorite")]
        public async Task<IActionResult> GetFavoriteById(int id)
        {
            var favoriteById = await _userService.GetFavorites(id);
            return Ok(favoriteById);
        }
        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetreviewsById(int id)
        {
            var reviewById = await _reviewService.GetAllReviewsByUserId(id);
            return Ok(reviewById);
        }

        [HttpPost]
        [Route("purchase")]

        public async Task<IActionResult> PurchaseMovie([FromBody] UserPurchaseRequestModel model)
        {

            var purchase = await _purchaseService.ConfirmPurchase(model);

            return Ok(purchase);
        }

        [HttpPost]
        [Route("review")]

        public async Task<IActionResult> PostReview([FromBody] ReviewRequestModel model)
        {
            var postreview = await _reviewService.PostReview(model);
            return Ok(postreview);
        }


        [HttpPut]
        [Route("review")]
        public async Task<IActionResult> PutReview([FromBody] ReviewRequestModel model)
        {
            var postreview = await _reviewService.PutReview(model);
            return Ok(postreview);
        }
    }
}
