#nullable disable

using Microsoft.Extensions.Options;
using System.Text.Json;

namespace RCL.AutoRenew.Function
{
    public class AuthTokenService : IAuthTokenService
    {
        private readonly IOptions<AuthorizationOptions> _authOptions;
        private static readonly HttpClient _httpClient;

        static AuthTokenService()
        {
            _httpClient = new HttpClient();
        }

        public AuthTokenService(IOptions<AuthorizationOptions> authOptions)
        {
            _authOptions = authOptions;
        }

        public async Task<AuthToken> GetAuthTokenAsync(string resource)
        {
            AuthToken authToken = new AuthToken();

            try
            {
                var formcontent = new List<KeyValuePair<string, string>>();
                formcontent.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                formcontent.Add(new KeyValuePair<string, string>("client_id", _authOptions.Value.client_id));
                formcontent.Add(new KeyValuePair<string, string>("client_secret", _authOptions.Value.client_secret));
                formcontent.Add(new KeyValuePair<string, string>("resource", resource));

                string url = $"https://login.microsoftonline.com/{_authOptions.Value.tenantId}/oauth2/token";

                var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(formcontent) };

                var response = await _httpClient.SendAsync(request);

                string jstr = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    authToken = JsonSerializer.Deserialize<AuthToken>(jstr);
                }
                else
                {
                    throw new Exception($"AutoRenewFunction could not obtain Access Token. {jstr}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"AutoRenewFunction Access Token Error : {ex.Message}");
            }

            return authToken;
        }
    }
}
