using AuthService.Application.Repositories;
using AuthService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthServiceDBContext _context;

        public UserRepository(AuthServiceDBContext context)
        {
            _context = context;
        }
        public Task<User> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Task.FromResult<User>(null);
            }
            else
            {
                return _context.Users
                    .Include(u => u.Roles)
                    .FirstOrDefaultAsync(u => u.Email == email);
            }

        }

        public Task<bool> RegisterUser(User user, string role)
        {
           Role r = _context.Roles.FirstOrDefault(r => r.Name == role);
            if (r == null)
            {
                return Task.FromResult(false);
            }
            user.Roles.Add(r);
            _context.Users.Add(user);
            _context.SaveChanges();
            return Task.FromResult(true);

        }
    }
}
