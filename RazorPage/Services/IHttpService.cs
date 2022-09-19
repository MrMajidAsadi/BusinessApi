namespace RazorPage.Services;

public interface IHttpService
{
    Task<T?> HttpGet<T>(string uri) where T : class;
}
