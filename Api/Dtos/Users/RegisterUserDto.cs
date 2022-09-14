using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Users;

#nullable disable
public class RegisterUserDto
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [Display(Name = "Password")]
    public string Password { get; set; }
}