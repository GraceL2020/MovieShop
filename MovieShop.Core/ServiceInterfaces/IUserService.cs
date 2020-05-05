using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IUserService
    {
        Task<User> ValidateUser(string email, string password);

        Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel);

        Task<User> GetUserByEmail(string email);

        Task<PurchaseResponseModel> GetAllPurchasedMoviesByUser(int id);
    }
}
