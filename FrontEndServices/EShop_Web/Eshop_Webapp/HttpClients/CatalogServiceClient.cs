using Eshop_Webapp.Models;
using System.Text.Json;

namespace Eshop_Webapp.HttpClients
{
    public class CatalogServiceClient
    {
        private readonly HttpClient _httpClient;
        public CatalogServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("ProductCatalog/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                if (responseContent != null)
                {
                    return JsonSerializer.Deserialize<IEnumerable<ProductModel>>(responseContent, new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true});
                }
            }
            return null;
        }
    }
}
