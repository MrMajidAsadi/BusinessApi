using Api.Dtos.Users;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class UsersController : ControllerBase
{
    private readonly SignInManager<OVitrinUser> _signInManager;
    private readonly UserManager<OVitrinUser> _userManager;

    public UsersController(
        SignInManager<OVitrinUser> signInManager,
        UserManager<OVitrinUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost]
    public virtual async Task<ActionResult<UserDto>> Register(RegisterUserDto dto)
    {
        try
        {
            var user = new OVitrinUser { UserName = dto.Email, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(new { message = result.Errors });
            
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok(new UserDto { Id = user.Id, UserName = user.UserName });
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Somthing went wrong" });
        }
    }
}