using CoreApi.Models;

namespace CoreApi.InterfacesWork
{
    public interface IDepartmentMaster
    {
        Task<IEnumerable<DepartmentModel>> GetDepartmentsAsync();
        Task<DepartmentModel> GetDepartmentByGuidAsync(string departmentGuid);
        Task<DepartmentModel> AddDepartmentAsync(DepartmentModel department);
        Task<DepartmentModel> UpdateDepartmentAsync(DepartmentModel department);
        Task<bool> DeleteDepartmentAsync(string departmentGuid);
    }
}
