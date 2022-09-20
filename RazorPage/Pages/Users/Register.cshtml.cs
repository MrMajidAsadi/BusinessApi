using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Services;

namespace RazorPage.Pages.Users;

public class RegisterModel : PageModel
{
    private readonly IHttpService _httpService;

    public RegisterModel(IHttpService httpService)
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

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [BindProperty]
    public string ConfirmPassword { get; set; } = string.Empty;

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var registerResponse = await _httpService.HttpPost<RegisterResponse>("Users", new 
        {
            Email = Email,
            Password = Password
        });

        if (registerResponse is null || registerResponse.Id == Guid.Empty) return Page();

        return RedirectToPage("Login");
    }
}

public class RegisterResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
}