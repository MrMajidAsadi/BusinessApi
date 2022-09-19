using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Models.Businesses;
using RazorPage.Services;

namespace RazorPage.Pages.Business;

public class BusinessIndexPageModel : PageModel
{
    private readonly IHttpService _httpService;

    public BusinessIndexPageModel(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public BusinessModel Business { get; set; } = new();

    public virtual async Task<IActionResult> OnGetAsync(int id)
    {
        var business = await _httpService.HttpGet<BusinessModel>($"Businesses/{id}");
        if (business is null) return NotFound();

        Business = business;
        return Page(); ;
    }
}