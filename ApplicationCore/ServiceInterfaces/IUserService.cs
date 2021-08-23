using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserLoginResponseModel> Login(UserLoginRequestModel requestModel);
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);

        Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMovies(int userId);

        Task<IEnumerable<MovieCardResponseModel>> GetFavorites(int userId);

        Task<UserLoginResponseModel> GetById(int id);



    }
}