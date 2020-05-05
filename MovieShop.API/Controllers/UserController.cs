using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}