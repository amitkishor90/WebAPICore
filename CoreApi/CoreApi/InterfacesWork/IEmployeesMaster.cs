using CoreApi.ApiResponse;
using CoreApi.Models;

namespace CoreApi.InterfacesWork
{
    public interface IEmployeesMaster
    {
        Task<ApiResponse<EmployeesModels>> AddEmployeeAsync(EmployeesModels employee);
        Task<ApiResponse<EmployeesModels>> GetEmployeeAsync(string employeeGuid);
        Task<ApiResponse<IEnumerable<EmployeesModels>>> GetEmployeesAsync();
        Task<ApiResponse<bool>> UpdateEmployeeAsync(EmployeesModels employee);
        Task<ApiResponse<bool>> DeleteEmployeeAsync(string employeeGuid);
    }
}
