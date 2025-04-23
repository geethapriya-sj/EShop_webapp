using AuthService.Domain.Models;

namespace AuthService.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);

        Task<bool> RegisterUser(User user,string role);
    }
}
