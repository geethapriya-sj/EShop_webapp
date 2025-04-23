using Eshop_Webapp.Models;
using System.Text;
using System.Text.Json;

namespace Eshop_Webapp.HttpClients
{
    public class AuthServiceClients
    {
        private readonly HttpClient _httpClient;
        public AuthServiceClients(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserModel> LoginAsync(LoginModel model)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("auth/login", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserModel>();
            }
            return null;
        }

        public async Task<bool> RegisterAsync(SignUpModel model)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsJsonAsync("auth/register", content);
            if (response.IsSuccessStatusCode)
            {
                string responsebody = await response.Content.ReadAsStringAsync();
                if (responsebody != null)
                {
                    return true;
                }

            }
            return false;

        }
    }
}
