using CoreApi.DatabaseModels;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.InterfaceImplements
{
    public class AdminInterfaceImplements : IadminInterface
    {
        private CurdApiContext appDbContext;
        private readonly ILogger<AdminInterfaceImplements> _logger;

        public   AdminInterfaceImplements(CurdApiContext appDbContext, ILogger<AdminInterfaceImplements> logger)
        {
            this.appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<bool> AuthenticateUser(LoginModel loginModel)
        {
            var user = await appDbContext.UserMasters.FirstOrDefaultAsync(u => u.Username == loginModel.Username);

            if (user == null)
            {
                return false; // User not found
            }
            else if (user.Password == loginModel.Password)
            {
                // Update last_login timestamp
                user.LastLogin = DateTime.Now;
                await appDbContext.SaveChangesAsync();
            }
            return true; // Authentication successful
        }
    }
}
