using MovieShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetTopGrossingMovies();

        Task<Movie> GetMovieById(int id);

        Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId);

    }
}
