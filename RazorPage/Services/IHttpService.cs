namespace RazorPage.Services;

public interface IHttpService
{
    Task<T?> HttpGet<T>(string uri) where T : class;
    Task<T?> HttpPost<T>(string uri, object dataToSend) where T : class;
}
