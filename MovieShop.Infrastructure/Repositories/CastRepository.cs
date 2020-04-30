using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Repositories
{
    public class CastRepository : EfRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Cast> GetByIdAsync(int id)
        {
            var cast = await _dbContext.Casts.Where(c => c.Id == id)
                            //.Include(c => c.MovieCasts)
                            //.ThenInclude(m => m.Movie)
                            .FirstOrDefaultAsync();
            return cast;
        }

        public async Task<IEnumerable<Object>> GetCastsForMovie(int movieId)
        {
            var casts = await _dbContext.MovieCasts.Where(mc => mc.MovieId == movieId)
                            .Include(mc => mc.Cast)
                            .Select(m => new { m.Cast.Id, m.Cast.Name,m.Cast.Gender, m.Cast.ProfilePath,m.Cast.TmdbUrl,m.Character})
                            .ToListAsync();
            
            return casts;
        }

        
    }
}
