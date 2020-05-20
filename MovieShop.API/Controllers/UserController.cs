using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.Entities;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Methods which are secured and should be presented with JWT to process that method
        // 1. Purchase a Movie
        // 2. Add Favorite
        // 3. Delete a Favorite
        // 4. Add a Review
        // 5. Update/Delete a a review
        // 6. Get all movies Purchased by user
        // 7. Get all movies FAvorited by user
        // 8. Get all reviews done by  user

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}/purchases")]
        public async Task<ActionResult> GetMoviesPurchasedByUser(int id)
        {
            var purchasedResponse = await _userService.GetAllPurchasedMoviesByUser(id);
            return Ok(purchasedResponse);
        }

        [Authorize]
        [HttpPost]
        [Route("purchase")]
        public async Task<ActionResult> PurchaseMovie([FromBody] PurchaseRequestModel purchaseRequest)
        {
            await _userService.PurchaseMovie(purchaseRequest);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}/favorites")]
        public async Task<ActionResult> GetAllFavoritesByUser(int id)
        {
            var favoriteResponse = await _userService.GetAllFavoritesForUser(id);
            return Ok(favoriteResponse);
        }

        [Authorize]
        [HttpPost]
        [Route("favorite")]
        public async Task<ActionResult> AddFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.AddFavorite(favoriteRequest);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("unfavorite")]
        public async Task<ActionResult> RemoveFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.RemoveFavorite(favoriteRequest);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<ActionResult> GetAllReviewsByUser(int id)
        {
            var reviewResponse = await _userService.GetAllReviewsByUser(id);
            return Ok(reviewResponse);
        }

        [Authorize]
        [HttpPost]
        [Route("review")]
        public async Task<ActionResult> AddMovieReview([FromBody] ReviewRequestModel reviewRequest)
        {
            await _userService.AddMovieReview(reviewRequest);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("{userId:int}/movie/{movieId:int}")]
        public async Task<ActionResult> DeleteMovieReview(int userId, int movieId)
        {
            await _userService.DeleteMovieReview(userId, movieId);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("review")]
        public async Task<ActionResult> UpdateMovieReview([FromBody] ReviewRequestModel reviewRequest)
        {
            await _userService.UpdateMovieReview(reviewRequest);
            return Ok();
        }

        // how to cal get all users by pagination
        //[Authorize]
        //[HttpGet]
        //[Route("{id:int}/users")]
        //public async Task<ActionResult> GetAllUsers(int id)
        //{
        //    var pagedResponse = await _userService.GetAllUsersByPagination(id);
        //    return Ok(pagedResponse);
        //}
        
       



    }
}