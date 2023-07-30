using CoreApi.ApiResponse;
using CoreApi.Models;

namespace CoreApi.InterfacesWork
{
    public interface IDepartmentMaster
    {
        Task<ApiResponse<DepartmentModel>> AddDepartmentAsync(DepartmentModel department);
        Task<ApiResponse<DepartmentModel>> GetDepartmentAsync(string departmentGuid);
        Task<ApiResponse<IEnumerable<DepartmentModel>>> GetDepartmentsAsync();
        Task<ApiResponse<DepartmentModel>> UpdateDepartmentAsync(DepartmentModel department);
        Task<ApiResponse<bool>> DeleteDepartmentAsync(string departmentGuid);
    }
}
