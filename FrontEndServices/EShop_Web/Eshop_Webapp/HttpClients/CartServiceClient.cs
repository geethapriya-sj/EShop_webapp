namespace Eshop_Webapp.HttpClients
{
    public class CartServiceClient
    {
        private readonly HttpClient _httpClient;

        public CartServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


    }
}
