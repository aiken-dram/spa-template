using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Common.Interfaces;
using Infrastructure.Common.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ApiController
{
    private readonly IAuthService _auth;
    private readonly ILogger _logger;

    public AuthController(IAuthService auth, ILogger<AuthController> logger)
    {
        _logger = logger;
        _auth = auth;
    }

    /// <summary>
    /// Authorizing user
    /// </summary>
    /// <param name="request">Authorization request</param>
    /// <response code="200">Successful authorization response</response>
    /// <response code="400">Authorization denied</response>
    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        _logger.LogDebug("Client ip {0}", ip);
        var vm = await _auth.LoginAsync(request, ip);

        /* lets generate token in webapi app since it's api related info */
        string userId = vm.User.UserID.ToString();
        string userName = vm.User.UserName;
        var userModules = vm.User.UserModules;

        // authentication successful so generate jwt token
        var tokenHandler = new JwtSecurityTokenHandler();
        //this should be at least 16 characters
        var key = Encoding.ASCII.GetBytes("key_which_i_think_should_not_be_here_2022");
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
        claims.Add(new Claim(ClaimTypes.Name, userName));
        foreach (string role in userModules)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        vm.Token = tokenHandler.WriteToken(token);

        return Ok(vm);
    }

    /// <summary>
    /// Getting user information for already authorized user
    /// </summary>
    /// <response code="200">User information</response>
    /// <response code="400">Error while retrieving user information</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponse>> Get()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var vm = await _auth.CurrentUserAsync(userId);

        return Ok(vm);
    }

    /// <summary>
    /// Check if token and user is valid
    /// </summary>
    /// <response code="200">User validated</response>
    /// <response code="400">User not validated</response>
    [Authorize]
    [HttpGet("Validate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Validate()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _auth.Validate(userId);
        return Ok();
    }
}
