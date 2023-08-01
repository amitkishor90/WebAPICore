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


        public async Task<ApiResponse<IEnumerable<DepartmentModel>>> GetDepartmentsAsync()
        {
            var response = new ApiResponse<IEnumerable<DepartmentModel>>();

            try
            {
                var departments = await appDbContext.Departments
                    .OrderBy(d => d.Name) // Assuming you want to order by the department name. Replace "Name" with the desired property.
                    .ToListAsync();
                if (departments == null || departments.Count == 0)
                {
                    response.Message = "No departments found.";
                    response.Status = "success"; // Since there is no error, the status can be "success" or "info" depending on the use case
                    return response;
                }
                var departmentModels = departments.Select(department => new DepartmentModel
                {
                    DepartmentGuid = department.DepartmentGuid.ToString(),
                    DepartmentName = department.Name
                });
                response.Data = departmentModels;
                response.Message = "Departments fetched successfully.";
                response.Status = "success";
            }
            catch (Exception ex)
            {
                response.Message = "Error while fetching departments.";
                response.Status = "error";
                response.ExceptionMessage = ex.Message;
            }
            return response;
        }




        public async Task<ApiResponse<DepartmentModel>> UpdateDepartmentAsync(DepartmentModel department)
        {
            var response = new ApiResponse<DepartmentModel>();

            try
            {
                // Find the department entity by its Guid in the database
                var existingDepartment = await appDbContext.Departments
                    .FirstOrDefaultAsync(d => d.DepartmentGuid == new Guid(department.DepartmentGuid));

                // If the department with the given Guid doesn't exist, return an error response
                if (existingDepartment == null)
                {
                    response.Message = "Department not found.";
                    response.Status = "error";
                    return response;
                }

                // Check if the department name already exists in the database
                var departmentNameExists = await DepartmentNameExistsAsync(department.DepartmentName, department.DepartmentGuid);
                if (departmentNameExists==true)
                {
                    response.Message = "Department name already exists.";
                    response.Status = "error";
                    return response;
                }

                // Update the properties of the existing department entity
                existingDepartment.Name = department.DepartmentName;

                // Save the changes to the database
                await appDbContext.SaveChangesAsync();

                // Set the success response with the updated department model
                response.Data = department;
                response.Message = "Department updated successfully.";
                response.Status = "success";
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the database operation
                // For example, log the error and set the error response
                response.Message = "Error while updating the department.";
                response.Status = "error";
                // You can also add an additional property to store the actual exception message if needed.
                response.ExceptionMessage = ex.Message;
            }

            return response;
        }

        // Other methods... for check 

        private async Task<bool> DepartmentNameExistsAsync(string departmentName, string departmentGuid)
        {
            // Check if a department with the given name exists in the database
           
            return await appDbContext.Departments.AnyAsync(d => d.Name.ToLower() == departmentName.ToLower() && d.DepartmentGuid.ToString() == departmentGuid);
        }

        public async Task<ApiResponse<bool>> DeleteDepartmentAsync(string departmentGuid)
        {
            var response = new ApiResponse<bool>();

            try
            {
                // Parse the departmentGuid to a Guid type
                if (!Guid.TryParse(departmentGuid, out Guid guid))
                {
                    response.Message = "Invalid departmentGuid format.";
                    response.Status = "error";
                    return response;
                }

                // Find the department entity by its Guid in the database
                var department = await appDbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentGuid == guid);

                // If the department with the given Guid doesn't exist, return an error response
                if (department == null)
                {
                    response.Message = "Department not found.";
                    response.Status = "error";
                    return response;
                }
                // Remove the department from the database
                appDbContext.Departments.Remove(department);
                await appDbContext.SaveChangesAsync();
                // Set the success response with a true value to indicate successful deletion
                response.Data = true;
                response.Message = "Department deleted successfully.";
                response.Status = "success";
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the database operation
                // For example, log the error and set the error response
                response.Message = "Error while deleting the department.";
                response.Status = "error";
                // You can also add an additional property to store the actual exception message if needed.
                response.ExceptionMessage = ex.Message;
            }
            return response;
        }
    }
}
