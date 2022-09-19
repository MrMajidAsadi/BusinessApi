using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Models.Businesses;
using RazorPage.Services;

namespace RazorPage.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpService _httpService;

    public IndexModel(ILogger<IndexModel> logger, IHttpService httpService)
    {
        _logger = logger;
        _httpService = httpService;
    }

    public List<BusinessModel> Businesses { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        var businesses = await _httpService.HttpGet<List<BusinessModel>>("Businesses");
        if (businesses is not null) Businesses = businesses;

        return Page();
    }
}
