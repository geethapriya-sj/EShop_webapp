using AutoMapper;
using CartService.Application.DTOs;
using CartService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Application.Mappers
{
    public class CartMapper: Profile
    {
        public CartMapper()
        {
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<CartItem, CartItemDTO>().ReverseMap();
        }
    }
   
}
