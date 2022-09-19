using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Pages.Business;

public class BusinessIndexPageModel : PageModel
{
    private readonly HttpClient _httpClient;

    public BusinessIndexPageModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public BusinessDto Business { get; set; } = new();

    public virtual async Task<IActionResult> OnGetAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7153/api/Businesses/{id}");
        if (!response.IsSuccessStatusCode)
            return NotFound();

        Business = await response.Content.ReadFromJsonAsync<BusinessDto>();
        if (Business is null)
            return NotFound();
        return Page(); ;
    }
}
public class BusinessDto
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public string? description { get; set; }
    public List<PictureDto> Pictures { get; set; }
}

public class PictureDto
{
    public int id { get; set; }
    public string url { get; set; }
}