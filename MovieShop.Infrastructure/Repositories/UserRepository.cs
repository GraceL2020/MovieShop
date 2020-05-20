using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieShop.Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {

        public UserRepository(MovieShopDbContext context) : base(context)
        {

        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users
                    .Include(u=>u.UserRoles).ThenInclude(ur=>ur.Role)
                    .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
