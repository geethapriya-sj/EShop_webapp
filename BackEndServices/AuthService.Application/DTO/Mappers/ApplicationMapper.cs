using AuthService.Domain.Models;
using AutoMapper;

namespace AuthService.Application.DTO.Mappers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<User, UserDTO>()
              .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(r => r.Name).ToList()))
              .ForMember(dest => dest.UserId,opt => opt.MapFrom(src=>src.Id));

            CreateMap<User,SignUpDTO>().ReverseMap();
        }
    }
}
