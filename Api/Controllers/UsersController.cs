using Api.Dtos.Users;
using ApplicationCore.Interfaces;
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
    private readonly ITokenClaimsService _tokenClaimService;

    public UsersController(
        SignInManager<OVitrinUser> signInManager,
        UserManager<OVitrinUser> userManager,
        ITokenClaimsService tokenClaimService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenClaimService = tokenClaimService;
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

    [HttpPost("Login")]
    public virtual async Task<IActionResult> Login(LoginUserDto loginUserDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password, false, true);

        if (!result.Succeeded)
            return Unauthorized();

        var token = await _tokenClaimService.GetTokenAsync(loginUserDto.Email);

        return Ok(new { Token = token });
    }
}