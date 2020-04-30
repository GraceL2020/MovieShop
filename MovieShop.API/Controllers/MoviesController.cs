using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            return Ok(movie);
        }

        [HttpGet]
        [Route("top")]
        public async Task<IActionResult> GetTopGrossingMovies()
        {
            var movies = await _movieService.GetTopGrossingMovies();
            return Ok(movies);
        }

        [HttpGet]
        [Route("genre/{genreId}")]
        public async Task<IActionResult> GetMoviesByGenreId(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenreId(genreId);
            return Ok(movies);
        }

        [HttpGet]
        [Route("cast/{castId}")]
        public async Task<IActionResult> GetMoviesForCast(int castId)
        {
            var movies = await _movieService.GetMoviesForCast(castId);
            return Ok(movies);
        }

    }
}