using eShopFlix.Web.Models;
using System.Text;
using System.Text.Json;

namespace eShopFlix.Web.HttpClients
{
    public class AuthServiceClient
    {
        HttpClient _client;
        public AuthServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserModel> LoginAsync(LoginModel model)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("auth/login", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<UserModel>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return user;
            }
            return null;
        }

        public async Task<bool> RegisterAsync(SignUpModel model)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("auth/register", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
