using CoreApi.InterfacesWork;
using CoreApi.Models;

namespace CoreApi.InterfaceImplements
{
    public class DepartmentInterfaceImplements : IDepartmentMaster
    {
        Task<DepartmentModel> IDepartmentMaster.AddDepartmentAsync(DepartmentModel department)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDepartmentMaster.DeleteDepartmentAsync(string departmentGuid)
        {
            throw new NotImplementedException();
        }

        Task<DepartmentModel> IDepartmentMaster.GetDepartmentByGuidAsync(string departmentGuid)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<DepartmentModel>> IDepartmentMaster.GetDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        Task<DepartmentModel> IDepartmentMaster.UpdateDepartmentAsync(DepartmentModel department)
        {
            throw new NotImplementedException();
        }
    }
}
