using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpRoyale.Services;
using Models = SharpRoyale.Models;

namespace SharpRoyale.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(TokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] Models.LoginDTO request)
    {
        // We assume a successful db verification here and proceed
        var token = tokenService.GenerateToken(request.Username);
        return Ok( new {Token = token});
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] Models.RegisterDTO request)
    {
        // We assume a successful db registation here and proceed
        var token = tokenService.GenerateToken(request.Username);
        Response.Cookies.Append("access_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false, 
            SameSite = SameSiteMode.Lax, 
            Expires = DateTimeOffset.UtcNow.AddHours(1)
        });
        return Ok();
    }

    [Authorize]
    [HttpGet("tokentest")]
    public IActionResult TokenTest()
    {
        return Ok();
    }
}