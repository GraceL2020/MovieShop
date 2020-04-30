using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService:IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _movieRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId)
        {
            return await _movieRepository.GetMoviesByGenre(genreId);
        }

        public async Task<IEnumerable<Movie>> GetTopGrossingMovies()
        {
            return await _movieRepository.GetTopGrossingMovies();
        }

        public async Task<IEnumerable<Movie>> GetMoviesForCast(int castId)
        {
            return await _movieRepository.GetMoviesForCast(castId);
        }
    }
}
