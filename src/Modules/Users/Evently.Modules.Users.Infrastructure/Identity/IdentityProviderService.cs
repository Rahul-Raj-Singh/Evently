
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Evently.Modules.Users.Infrastructure.Identity;

public class IdentityProviderService(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration,
    ILogger<IdentityProviderService> logger
) : IIdentityProviderService
{
    public async Task<Result<string>> RegisterUserAsync(string email, string password, string firstName, string lastName)
    {
        try
        {
            // OAuth: client credentials flow
            var accessToken = await AuthenticateAndGetAccessToken();

            // use the access token to create user
            var identityId = await CreateUser(accessToken, email, password, firstName, lastName);

            return Result.Success(identityId);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
        {
            return Result.Failure<string>(Error.Conflict("Users.Register", "Email is duplicate!"));
        }
    }

    private async Task<string> CreateUser(string accessToken, string email, string password, string firstName, string lastName)
    {
        var adminUrl = configuration.GetValue<string>("Users:AdminUrl")!;

        using var httpClient = httpClientFactory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Post, new Uri(adminUrl));

        request.Content = new StringContent(JsonConvert.SerializeObject(
            new JObject
            {
                { "username", email },
                { "email", email },
                { "firstName", firstName },
                { "lastName", lastName },
                { "emailVerified", true },
                { "enabled", true },
                { "credentials", new JArray 
                    {
                        new JObject
                        {
                            { "type", "password" },
                            { "value", password },
                            {"temporary", false}
                        }
                    } 
                },
            }
        ), new MediaTypeHeaderValue("application/json"));

        request.Headers.Authorization = new("Bearer", accessToken);

        var response = await httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var location = response.Headers.Location!.PathAndQuery!;
        var index = location.IndexOf("users/", StringComparison.InvariantCultureIgnoreCase);
        var identityId = location.Substring(index + "users/".Length);

        return identityId;
    }

    private async Task<string> AuthenticateAndGetAccessToken()
    {
        var tokenUrl = configuration.GetValue<string>("Users:TokenUrl")!;
        var clientId = configuration.GetValue<string>("Users:ConfidentialClientId")!;
        var clientSecret = configuration.GetValue<string>("Users:ConfidentialClientSecret")!;
        var grantType = "client_credentials";
        var scope =  "openid";

        var kvPairs = new List<KeyValuePair<string, string>>
        {
            new("client_id", clientId),
            new("client_secret", clientSecret),
            new("grant_type", grantType),
            new("scope", scope),
        };

        using var httpClient = httpClientFactory.CreateClient();

        using var request = new HttpRequestMessage(HttpMethod.Post, new Uri(tokenUrl))
        {
            Content = new FormUrlEncodedContent(kvPairs)
        };

        var response = await httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        var accessToken =  JObject.Parse(jsonString).Value<string>("access_token")!;

        return accessToken;
    }
}