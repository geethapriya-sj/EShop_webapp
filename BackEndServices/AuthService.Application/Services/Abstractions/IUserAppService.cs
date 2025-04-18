using AuthService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Services.Abstractions
{
    public interface IUserAppService
    {
        bool RegisterUser(SignUpDTO signUpDTO);

        UserDTO LoginUser(LoginDTO loginDTO);
    }
}
