using CartService.Application.DTOs;

namespace CartService.Application.Service.Abstractions
{
    public interface ICartAppService
    {
        Task<CartDTO> GetUserCart(long UserId);
        int GetCartItemCount(long UserId);
        IEnumerable<CartItemDTO> GetCartItems(long CartId);
        Task<CartDTO> GetCart(int CartId);
        CartDTO AddItem(long UserId, long CartId, int ItemId, decimal UnitPrice, int Quantity);
        int DeleteItem(int CartId, int ItemId);
        bool MakeInActive(int CartId);
        int UpdateQuantity(int CartId, int ItemId, int Quantity);
    }
}
