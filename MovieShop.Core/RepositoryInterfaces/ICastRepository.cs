using MovieShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface ICastRepository: IAsyncRepository<Cast>
    {
        Task<IEnumerable<Movie>> GetMoviesForCast(int castId);
    }
}
