using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;
using MovieShop.Core.Helpers;
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

        Task<PagedResultSet<User>> GetAllUsersByPagination(int pageSize = 20, int page = 0, string lastName = "");
        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> FavoriteExists(int id, int movieId);
        Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);
        Task PurchaseMovie(PurchaseRequestModel purchaseRequest);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest);
        Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);
        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task DeleteMovieReview(int userId, int movieId);
        Task<ReviewResponseModel> GetAllReviewsByUser(int id);
    }
}
