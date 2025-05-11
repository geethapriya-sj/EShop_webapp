using eShopFlix.Web.Models;
using System.Text.Json;

namespace eShopFlix.Web.HttpClients
{
    public class CatalogServiceClient
    {
        HttpClient _client;
        public CatalogServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            HttpResponseMessage response = await _client.GetAsync("catalog/getall");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent != null)
                {
                    return JsonSerializer.Deserialize<IEnumerable<ProductModel>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            return null;
        }
    }
}
