using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Pages.Business;

namespace RazorPage.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _httpClient;

    public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public List<BusinessDto> Businesses { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var response = await _httpClient.GetAsync("https://localhost:7153/api/Businesses");
        if (!response.IsSuccessStatusCode)
            return NotFound();
        
        Businesses = await response.Content.ReadFromJsonAsync<List<BusinessDto>>();
        return Page();
    }
}
