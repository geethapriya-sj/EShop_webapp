using AuthService.Application.DTOs;
using AuthService.Application.Repositories;
using AuthService.Application.Services.Abstractions;
using AuthService.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using BC = BCrypt.Net.BCrypt;

namespace AuthService.Application.Services.Implementations
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        IMapper _mapper;
        public UserAppService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public UserDTO LoginUser(LoginDTO loginDTO)
        {
            User user = _userRepository.GetUserByEmail(loginDTO.Email).Result;
            if (user != null)
            {
                bool isValidPassword = BC.Verify(loginDTO.Password, user.Password);
                if (isValidPassword)
                {
                    UserDTO userDTO = _mapper.Map<UserDTO>(user);
                    userDTO.Token = GenerateJwtToken(userDTO);
                    return userDTO;
                }                
            }
            return null;
        }

        private string GenerateJwtToken(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            int ExpireMinutes = Convert.ToInt32(_configuration["Jwt:ExpireMinutes"]);

            var claims = new[] {
                             new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                             new Claim(JwtRegisteredClaimNames.Email, user.Email),
                             new Claim("Roles", string.Join(",",user.Roles)),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                             };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                            _configuration["Jwt:Audience"],
                                            claims,
                                            expires: DateTime.UtcNow.AddMinutes(ExpireMinutes), //token expiry minutes
                                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool RegisterUser(SignUpDTO signUpDTO)
        {
            User userData = _userRepository.GetUserByEmail(signUpDTO.Email).Result;
            if (userData == null)
            {
                User user = _mapper.Map<User>(signUpDTO);
                user.Password = BC.HashPassword(signUpDTO.Password);
                user.CreatedDate = DateTime.UtcNow;
                user.EmailConfirmed = false;

                bool result = _userRepository.RegisterUser(user, signUpDTO.Role).Result;
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
