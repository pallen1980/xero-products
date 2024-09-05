using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XeroProducts.BL.Interfaces;

[ApiController]
[Route("api/product")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenProvider _jwtTokenProvider;
    public AuthController(IJwtTokenProvider jwtTokenProvider)
    {
        _jwtTokenProvider = jwtTokenProvider;
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<ActionResult<string>> Login([FromBody] UserCredential userCredential)
    {
        //TODO: Add sign-in logic here (match user/pass against credentials in DB)



        var hasLogonSucceeded = true;
      
        //If the logon failed, raise an unauthorised exception
        if (!hasLogonSucceeded)
        {
            throw new UnauthorizedAccessException();
        }

        //logon was successful... Generate and return a JwtToken for the user
        var token = _jwtTokenProvider.GenerateJwtToken(userCredential.Username);

        return Ok(token);
    }
}