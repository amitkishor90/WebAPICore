using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class LoginController : ControllerBase
    {
        private readonly ILogger _loginService;
        private readonly IadminInterface _IadminInterface;

        public LoginController(IadminInterface IadminInterface)
        {
            
            this._IadminInterface = IadminInterface;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _IadminInterface.AuthenticateUser(model);

            if (response.IsError)
            {
                return Unauthorized(new { message = "Authentication failed." });
            }

            return Ok(response.Data);
        }

    }
}
