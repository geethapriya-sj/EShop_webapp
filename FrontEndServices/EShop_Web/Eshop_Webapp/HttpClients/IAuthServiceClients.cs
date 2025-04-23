using Eshop_Webapp.Models;
using System.Threading.Tasks;

namespace Eshop_Webapp.HttpClients
{
    public interface IAuthServiceClients
    {
        Task<UserModel> LoginAsync(LoginModel model);
        Task<bool> RegisterAsync(SignUpModel model);
    }
}
