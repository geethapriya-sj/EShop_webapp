using AuthService.Application.DTO;
using AuthService.Application.Repositories;
using AuthService.Application.Services.Abstractions;
using AuthService.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace AuthService.Application.Services.Implementation
{
    public class UserAppService: IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserAppService(IUserRepository userRepository,IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public bool RegisterUser(SignUpDTO signUpDTO)
        {
            var userData = _userRepository.GetUserByEmail(signUpDTO.Email).Result;
            if(userData == null)
            {
                User user = _mapper.Map<User>(signUpDTO);
                user.Password = BC.HashPassword(signUpDTO.Password);
                user.CreatedDate = DateTime.UtcNow;
                user.EmailConfirmed = false;
                bool isRegistered = _userRepository.RegisterUser(user, signUpDTO.Role).Result;
                return isRegistered;
            }
            else
            {
                return false;
            }
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            int expiryTime = int.Parse(_configuration["Jwt:ExpireTime"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Roles", string.Join(",",user.Roles))
            };

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryTime),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
