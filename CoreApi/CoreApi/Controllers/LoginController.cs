using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger _loginService;
        private readonly IadminInterface _IadminInterface;

        public LoginController(IadminInterface IadminInterface)
        {
            
            this._IadminInterface = IadminInterface;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isAuthenticated = await _IadminInterface.AuthenticateUser(loginModel);

            if (isAuthenticated)
            {
                return Ok("Authentication successful!");
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }

    }
}
