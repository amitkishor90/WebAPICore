using CoreApi.ApiResponse;
using CoreApi.DatabaseModels;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoreApi.InterfaceImplements
{
    public class DepartmentInterfaceImplements : IDepartmentMaster
    {
        private CurdApiContext appDbContext;
        private readonly ILogger<GenderInterfaceImplements> _logger;

        public DepartmentInterfaceImplements(CurdApiContext appDbContext, ILogger<GenderInterfaceImplements> logger)
        {
            this.appDbContext = appDbContext;
            _logger = logger;
        }

        private async Task<bool> DepartmentNameExistsAsync(string departmentName)
        {
            // Convert the department name to lowercase for case-insensitive comparison (optional)
            departmentName = departmentName?.ToLower();

            // Check if any department in the database has the same name
            return await appDbContext.Departments.AnyAsync(d => d.Name.ToLower() == departmentName);
        }


        public async Task<ApiResponse<DepartmentModel>> AddDepartmentAsync(DepartmentModel department)
        {
            var response = new ApiResponse<DepartmentModel>();

            try
            {
                // Check if a department with the same name already exists
                if (await DepartmentNameExistsAsync(department.DepartmentName))
                {
                    response.Message = "Department with the same name already exists.";
                    response.Status = "error";
                    return response;
                }

                // Create a new Department entity from the DepartmentModel
                var newDepartment = new Department
                {
                    DepartmentGuid = Guid.NewGuid(),
                    Name = department.DepartmentName
                };

                // Add the new department to the database
                appDbContext.Departments.Add(newDepartment);
                await appDbContext.SaveChangesAsync();

                // Set the success response
                response.Data = new DepartmentModel
                {
                    DepartmentGuid = newDepartment.DepartmentGuid.ToString(),
                    DepartmentName = newDepartment.Name
                };
                response.Message = "Department added successfully.";
                response.Status = "success";
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the database operation
                // For example, log the error and set the error response
                response.Message = "Error while adding a new department.";
                response.Status = "error";
                // You can also add an additional property to store the actual exception message if needed.
                response.ExceptionMessage = ex.Message;
            }

            return response;
        }


        public async Task<ApiResponse<DepartmentModel>> GetDepartmentAsync(string departmentGuid)
        {
            var response = new ApiResponse<DepartmentModel>();

            try
            {
                // Parse the departmentGuid to a Guid type
                if (!Guid.TryParse(departmentGuid, out Guid guid))
                {
                    response.Message = "Invalid departmentGuid format.";
                    response.Status = "error";
                    return response;
                }
                // Query the database to find the department with the given departmentGuid
                var department = await appDbContext.Departments
                    .FirstOrDefaultAsync(d => d.DepartmentGuid == guid);

                // Check if the department with the specified departmentGuid exists
                if (department == null)
                {
                    response.Message = "Department not found.";
                    response.Status = "error";
                    return response;
                }

                // Create a new DepartmentModel with the department details
                var departmentModel = new DepartmentModel
                {
                    DepartmentGuid = department.DepartmentGuid.ToString(),
                    DepartmentName = department.Name
                };

                // Set the success response
                response.Data = departmentModel;
                response.Message = "Department fetched successfully.";
                response.Status = "success";
                response.IsError = false; // Set to false for successful response

            }
            catch (Exception ex)
            {

                // Handle any exceptions that occur during the database operation
                // For example, log the error and set the error response
                response.Message = "Error while fetching the department.";
                response.Status = "error";
                // You can also add an additional property to store the actual exception message if needed.
                response.ExceptionMessage = ex.Message;
            }

            return response;
        }


        public Task<ApiResponse<IEnumerable<DepartmentModel>>> GetDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<DepartmentModel>> UpdateDepartmentAsync(DepartmentModel department)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> DeleteDepartmentAsync(string departmentGuid)
        {
            throw new NotImplementedException();
        }
    }
}
