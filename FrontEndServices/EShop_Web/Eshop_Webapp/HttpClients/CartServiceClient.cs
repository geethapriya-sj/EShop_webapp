using Eshop_Webapp.Models;
using System.Text.Json;

namespace Eshop_Webapp.HttpClients
{
    public class CartServiceClient
    {
        private readonly HttpClient _httpClient;

        public CartServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartModel> AddToCartAsync(long userId, CartItemModel cartitem )
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(cartitem), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("cart/additem/" + userId, content);
            if(response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<CartModel>();
                return responseContent;
            }
            return null;
        }

        //getcart post methof
        public async Task<CartModel> GetCartAsync(long userId)
        {
            var response = await _httpClient.GetAsync($"cart/getcart/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent != null)
                {
                    return JsonSerializer.Deserialize<CartModel>(responseContent, new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true });
                }
            }
            return null;
        }
        //getcart by carid
        public async Task<CartModel> GetCartByIdAsync(int cartId)
        {
            var cart = await _httpClient.GetFromJsonAsync<CartModel>($"cart/getcart/{cartId}");
            if (cart != null)
            {
                return cart;
            }
            return null;
        }

        //make cart inactive
        public async Task<bool> MakeCartInactiveAsync(int cartId)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"cart/makeinactive/{cartId}");
        }

        //delete cart item
        public async Task<int> DeleteCartItemAsync(int cartId, int itemId)
        {
            return await _httpClient.DeleteFromJsonAsync<int>($"cart/deleteitem/{cartId}/{itemId}");
        }

        //update quantity
        public async Task<int> UpdateQuantityAsync(int quantity, long cartId, long itemID)
        {
            return await _httpClient.GetFromJsonAsync<int>($"cart/updatequantity/{cartId}/{itemID}/{quantity}");
        }

        //get cart itemcount
         public async Task<int> GetCartItemCountAsync(long userId)
        {
            return await _httpClient.GetFromJsonAsync<int>($"cart/getcartitemcount/{userId}");
        }
    }
}
