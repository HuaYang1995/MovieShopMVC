using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
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
        public class PurchaseService : IPurchaseService
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly IPurchaseRepository _purchaseRepository;
            private readonly IMovieService _movieService;
            private readonly IUserService _userService;
            public PurchaseService(ICurrentUserService currentUserService, IPurchaseRepository purchaseRepository, IMovieService movieService,
                                    IUserService userService)
            {
                _purchaseRepository = purchaseRepository;
                _movieService = movieService;
                _currentUserService = currentUserService;
                _userService = userService;
            }

            public async Task<Purchase> ConfirmPurchase(UserPurchaseRequestModel model)
            {
                var dbUser = await _purchaseRepository.GetByIdAsync(model.UserId);
                var dbMovie = await _purchaseRepository.GetByIdAsync(model.MovieId);

                if (dbUser != null && dbMovie != null)
                {
                    throw new ConflictException("User already purchased this movie");
                }

                var userId = _currentUserService.UserId;
                var user = await _userService.GetById(model.UserId);
                var movie = await _movieService.GetMovieDetails(model.MovieId);
                var purchase = new Purchase
                {

                    UserId = user.Id,
                    TotalPrice = movie.Price.GetValueOrDefault(),
                    MovieId = movie.Id,
                    PurchaseNumber = model.PurchaseNumber,
                    PurchaseDateTime = model.PurchaseDateTime,
                };

                var createdPurchase = await _purchaseRepository.AddAsync(purchase);

                return createdPurchase;
            }

        public async Task<List<PurchaseResponsemodel>> GetPurchase()
        {
            var purchases= await _purchaseRepository.GetAllPurchases();
            var purchaseslist = new List<PurchaseResponsemodel>();
            foreach (var purchase in purchases)
            {
                purchaseslist.Add(new PurchaseResponsemodel
                {
                    UserId = purchase.UserId,
                    PurchaseNumber = purchase.PurchaseNumber,
                    TotalPrice = purchase.TotalPrice,
                    PurchaseDateTime = purchase.PurchaseDateTime,
                    MovieId = purchase.MovieId
                });
            }
            return purchaseslist;
        }
    }
}
