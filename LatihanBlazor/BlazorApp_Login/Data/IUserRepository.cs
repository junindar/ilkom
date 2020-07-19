using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAPP_Login.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlazorAPP_Login.Data
{
    public interface IUserRepository
    {
         Task<User> CheckLoginAsync(User obj);
    }

    public class UserRepository : IUserRepository
    {
        private readonly PustakaDbContext _dbContext;

        public UserRepository(PustakaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> CheckLoginAsync(User obj)
        {
            var user = await _dbContext.Users.Where(c => c.Username.ToLower() == obj.Username.ToLower() &&
                                                         c.Password==obj.Password).FirstOrDefaultAsync();

            return user;
        }
    }

}
