using AuthService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Services.Abstractions
{
    public interface IUserAppService
    {
        UserDTO LoginUser(LoginDTO loginDTO);
        bool RegisterUser(SignUpDTO signUpDTO);
    }
}
