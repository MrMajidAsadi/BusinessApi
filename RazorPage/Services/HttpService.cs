using System.Text;
using System.Text.Json;

namespace RazorPage.Services;

public class HttpService : IHttpService
{
    #region Fields

    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    #endregion

    public HttpService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiUrl = configuration.GetSection("ApiBase").Value;
    }

    private async Task<T?> FromHttpResponseMessage<T>(HttpResponseMessage result)
    {
        return await result.Content.ReadFromJsonAsync<T>(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    private StringContent ToJson(object obj)
    {
        return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
    }

    public virtual async Task<T?> HttpGet<T>(string uri) where T : class
    {
        var result = await _httpClient.GetAsync($"{_apiUrl}{uri}");
        if (!result.IsSuccessStatusCode) return null;

        return await FromHttpResponseMessage<T>(result);
    }

    public virtual async Task<T?> HttpPost<T>(string uri, object dataToSend) where T : class
    {
        var content = ToJson(dataToSend);
        var result = await _httpClient.PostAsync($"{_apiUrl}{uri}", content);

        if (!result.IsSuccessStatusCode) return null;

        return await FromHttpResponseMessage<T>(result);
    }
}