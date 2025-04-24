using AutoMapper;
using CartService.Application.DTOs;
using CartService.Application.Repositories;
using CartService.Application.Service.Abstractions;
using CartService.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Application.Service.Implementation
{
    public class CartAppService: ICartAppService
    {
        ICartRepository _cartRepository;
        IMapper _mapper;
        IConfiguration _configuration;
        public CartAppService(ICartRepository cartRepository, IMapper mapper, IConfiguration configuration)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        private CartDTO PopulateCartDetails(Cart cart)
        {
            try
            {
                CartDTO cartModel = _mapper.Map<CartDTO>(cart);

                var productIds = cart.CartItems.Select(x => x.ItemId).ToList();

                //Need to change
                //var tasks = productIds.Select(id => _catalogServiceClient.GetAsync(id)).ToList();
                //var products = Task.WhenAll(tasks).Result;

                if (cartModel.CartItems.Count > 0)
                {
                    //cartModel.CartItems.ForEach(x =>
                    //{
                    //    var product = products.FirstOrDefault(p => p.ProductId == x.ItemId);
                    //    if (product != null)
                    //    {
                    //        x.Name = product.Name;
                    //        x.ImageUrl = product.ImageUrl;
                    //    }
                    //});

                    foreach (var item in cartModel.CartItems)
                    {
                        cartModel.Total += item.UnitPrice * item.Quantity;
                    }
                    cartModel.Tax = cartModel.Total * Convert.ToDecimal(_configuration["Tax"]) / 100;
                    cartModel.GrandTotal = cartModel.Total + cartModel.Tax;
                }
                return cartModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CartDTO AddItem(long UserId, long CartId, int ItemId, decimal UnitPrice, int Quantity)
        {
            CartItem cartItem = new CartItem()
            {
                CartId = CartId,
                ItemId = ItemId,
                Quantity = Quantity,
                UnitPrice = UnitPrice
            };
            var cart = _cartRepository.AddItem(CartId, UserId, cartItem);
            return _mapper.Map<CartDTO>(cart);
        }

        public int DeleteItem(int CartId, int ItemId)
        {
            return _cartRepository.DeleteItem(CartId, ItemId);
        }

        public Task<CartDTO> GetCart(int CartId)
        {
            Cart cart = _cartRepository.GetCart(CartId);
            if (cart == null)
            {
                return null;
            }
            CartDTO cartModel = PopulateCartDetails(cart);
            return Task.FromResult(cartModel);
        }

        public int GetCartItemCount(long UserId)
        {
            if (UserId > 0)
                return _cartRepository.GetCartItemCount(UserId);
            return 0;
        }

        public IEnumerable<CartItemDTO> GetCartItems(long CartId)
        {
            var data = _cartRepository.GetCartItems(CartId);
            return _mapper.Map<IEnumerable<CartItemDTO>>(data);
        }

        public Task<CartDTO> GetUserCart(long UserId)
        {
            Cart cart = _cartRepository.GetUserCart(UserId);
            if (cart != null)
            {
                CartDTO cartModel = PopulateCartDetails(cart);
                return Task.FromResult(cartModel);
            }
            return Task.FromResult<CartDTO>(null);
        }

        public bool MakeInActive(int CartId)
        {
            return _cartRepository.MakeInActive(CartId);
        }

        public int UpdateQuantity(int CartId, int ItemId, int Quantity)
        {
            return _cartRepository.UpdateQuantity(CartId, ItemId, Quantity);
        }
    }
}
