using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XeroProducts.BL.Interfaces;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly Lazy<IJwtTokenProvider> _jwtTokenProvider;
    private readonly Lazy<IUserProvider> _userProvider;

    protected IJwtTokenProvider JwtTokenProvider => _jwtTokenProvider.Value;
    protected IUserProvider UserProvider => _userProvider.Value;

    public AuthController(Lazy<IUserProvider> userProvider,
                          Lazy<IJwtTokenProvider> jwtTokenProvider)
    {
        _userProvider = userProvider;
        _jwtTokenProvider = jwtTokenProvider;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public virtual async Task<ActionResult<string>> Login([FromBody] UserCredentialFormModel userCredential)
    {
        // Attempt to match usercredentials to a user
        var user = await UserProvider.VerifyUserCredentials(userCredential.Username, userCredential.Password);

        // No matching user... raise an unauthorised exception
        if (user == null)
        {
            throw new UnauthorizedAccessException();
        }

        //logon was successful... Generate and return a JwtToken for the user
        var token = JwtTokenProvider.GenerateJwtToken(userCredential.Username);

        return Ok(token);
    }
}