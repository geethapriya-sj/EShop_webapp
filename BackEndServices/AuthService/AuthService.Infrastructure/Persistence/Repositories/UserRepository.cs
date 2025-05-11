using AuthService.Application.Repositories;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        AuthServiceDbContext _dbContext;
        public UserRepository(AuthServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<bool> RegisterUser(User user, string role)
        {
            Role r = _dbContext.Roles.FirstOrDefault(r => r.Name == role);
            if(r!=null)
            {
                user.Roles.Add(r);
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
