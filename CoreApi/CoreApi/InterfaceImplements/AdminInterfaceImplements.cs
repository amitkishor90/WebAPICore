using CoreApi.ApiResponse;
using CoreApi.DatabaseModels;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreApi.InterfaceImplements
{
    public class AdminInterfaceImplements : IadminInterface
    {
        private CurdApiContext appDbContext;
        private readonly ILogger<AdminInterfaceImplements> _logger;
        public readonly IConfiguration _configuration;
      //  private readonly SignInManager<AdminInterfaceImplements> _signInManager;
        public AdminInterfaceImplements(CurdApiContext appDbContext, ILogger<AdminInterfaceImplements> logger, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            _logger = logger;
            _configuration = configuration;
            
        }
        public async Task<ApiResponse<loginresponse>> AuthenticateUser(LoginModel loginModel)
        {
            var response = new ApiResponse<loginresponse>();
            var user = await appDbContext.UserMasters.FirstOrDefaultAsync(u => u.Username == loginModel.Username);
            try
            {
                // Perform user authentication here
              
                if (user == null)
                {
                    response.Message = "User not found.";
                    response.IsError = true;
                    response.Status = "Error";
                    return response;
                }
                if (!await IsPasswordValid(loginModel.Username,user.Password))
                {
                    response.Message = "Invalid username or password.";
                    response.IsError = true;
                    response.Status = "Error";
                    return response;
                }



                // User is authenticated, generate JWT token
                var token = GenerateJWT(user.Username, Convert.ToString(user.Id)); // Implement the token generation method

                var loginModel_ = new loginresponse
                {
                    Username = user.Username,
                    Token = token
                };

                response.Data = loginModel_;
                response.Message = "Authentication successful.";
                response.IsError = false;
                response.Status = "Success";
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred during authentication.";
                response.IsError = true;
                response.Status = "Error";
                response.ExceptionMessage = ex.Message;
            }

            return response;

        }


        public string GenerateJWT(string Username, string EmpID)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //claim   used to add identity to JWT token
         var claims = new[] {
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
         new Claim(JwtRegisteredClaimNames.Sid,   EmpID),
         new Claim(JwtRegisteredClaimNames.UniqueName, Username),
      //   new Claim(ClaimTypes.Role,Role),
         new Claim("Date", DateTime.Now.ToString()),
         };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audiance"],
              claims,    //null original value
              expires: DateTime.Now.AddMinutes(120),
              //notBefore:
              signingCredentials: credentials);
            string Data = new JwtSecurityTokenHandler().WriteToken(token); //return access token 
            return Data;
        }


        private async Task<bool> IsPasswordValid(string user, string password)
        {
            var storedUser = await appDbContext.UserMasters.FirstOrDefaultAsync(u => u.Username == user);

            if (storedUser == null)
            {
                return false; // User not found, password cannot be valid
            }



            return storedUser.Password == password;
        }


    }
}
