using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Services;

namespace RazorPage.Pages.Users;

public class LogInModel : PageModel
{
    private readonly IHttpService _httpService;

    public LogInModel(IHttpService httpService)
    {
        _httpService = httpService;
    }

    [Required]
    [EmailAddress]
    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [BindProperty]
    public string Password { get; set; } = string.Empty;
    
    [BindProperty]
    public bool RememberMe { get; set; }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var response = await _httpService.HttpPost<LogInResponse>("Users/Login", new
        {
            Email,
            Password
        });

        if (response is null || string.IsNullOrEmpty(response.Token)) return Page();

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.Name, Email));
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        HttpContext.Session.SetString("TokenForApi", response.Token);

        return RedirectToPage("../Index");
    }
}

public class LogInResponse
{
    public string Token { get; set; } = string.Empty;
}