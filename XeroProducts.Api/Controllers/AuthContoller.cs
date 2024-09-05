using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XeroProducts.BL.Interfaces;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IUserProvider _userProvider;

    public AuthController(IUserProvider userProvider,
                          IJwtTokenProvider jwtTokenProvider)
    {
        _userProvider = userProvider;
        _jwtTokenProvider = jwtTokenProvider;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] UserCredential userCredential)
    {
        // Attempt to match usercredentials to a user
        var user = await _userProvider.VerifyUserCredentials(userCredential.Username, userCredential.Password);

        // No matching user... raise an unauthorised exception
        if (user == null)
        {
            throw new UnauthorizedAccessException();
        }

        //logon was successful... Generate and return a JwtToken for the user
        var token = _jwtTokenProvider.GenerateJwtToken(userCredential.Username);

        return Ok(token);
    }
}