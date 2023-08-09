using CoreApi.ApiResponse;
using CoreApi.Models;

namespace CoreApi.InterfacesWork
{
    public interface IadminInterface
    {
        Task<ApiResponse<loginresponse>> AuthenticateUser(LoginModel loginModel);
    }
}
