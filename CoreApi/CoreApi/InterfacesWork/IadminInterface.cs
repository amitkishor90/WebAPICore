using CoreApi.Models;

namespace CoreApi.InterfacesWork
{
    public interface IadminInterface
    {
        Task<bool> AuthenticateUser(LoginModel loginModel);
    }
}
