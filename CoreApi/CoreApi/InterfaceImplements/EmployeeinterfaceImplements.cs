using CoreApi.ApiResponse;
using CoreApi.DatabaseModels;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
 

namespace CoreApi.InterfaceImplements
{
    public class EmployeeinterfaceImplements : IEmployeesMaster
    {

        private CurdApiContext appDbContext;
        private Guid? genderGuid;
        private readonly ILogger<EmployeeinterfaceImplements> _logger;
       

        
        public EmployeeinterfaceImplements(CurdApiContext appDbContext, ILogger<EmployeeinterfaceImplements> logger)
        {
            this.appDbContext = appDbContext;
            _logger = logger;
            

        }

        public async Task<ApiResponse<EmployeesModels>> AddEmployeeAsync(EmployeesModels employee)
        {
            var response = new ApiResponse<EmployeesModels>();

           int GenderID= GetGenderID(employee.GenderGuid  );
            int DepartmentID = GetDepartmentID(employee.DepartmentGuid);
            try
            {
                // Check if the PenCardNo is already used by another employee
                var isDuplicatePenCardNo = await appDbContext.Employees
                    .AnyAsync(e => e.PenCardNo == employee.PenCardNo);

                if (isDuplicatePenCardNo)
                {
                    response.Message = "PenCardNo is already used by another employee.";
                    response.Status = "error";
                    return response;
                }
                // for gender guid and department id 


                // Create a new Employee entity from the EmployeesModels
                var newEmployee = new Employee
                {
                    Guid = Guid.NewGuid(),
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Emailid = employee.Emailid,
                    PenCardNo = employee.PenCardNo,
                    Salary =   employee.Salary,
                    GenderId = GenderID,
                    DepartmentId = DepartmentID,
                    Address = employee.Address,
                    Role = employee.Role,
                    DateIns = DateTime.Now
                };

                appDbContext.Employees.Add(newEmployee);
                await appDbContext.SaveChangesAsync();

                // Set the success response with the newly added employee
                response.Data = new EmployeesModels
                {
                    Guid = Convert.ToString( Guid.NewGuid()),
                    FirstName = newEmployee.FirstName,
                    LastName = newEmployee.LastName,
                    Emailid = newEmployee.Emailid,
                    PenCardNo = newEmployee.PenCardNo,
                    Salary = newEmployee.Salary,
                   // GenderId = newEmployee.GenderId,
                   // DepartmentId = newEmployee.DepartmentId,
                    Address = newEmployee.Address,
                    Role = newEmployee.Role,
                    DateIns = newEmployee.DateIns
                };
                response.Message = "Employee added successfully.";
                response.Status = "success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an employee.");
                response.Message = "Error while adding an employee.";
                response.Status = "error";
                // You can also add an additional property to store the actual exception message if needed.
                response.ExceptionMessage = ex.Message;
            }

            return response;
        }

        public Task<ApiResponse<bool>> DeleteEmployeeAsync(string employeeGuid)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<EmployeesModels>> GetEmployeeAsync(string employeeGuid)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<EmployeesModels>>> GetEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> UpdateEmployeeAsync(EmployeesModels employee)
        {
            throw new NotImplementedException();
        }

        public int GetGenderID(string _GenderGuid)
        {
            Guid guid = Guid.Parse(_GenderGuid);
            var gender = appDbContext.Genders.FirstOrDefault(g => g.GenderGuid == guid);
            return gender.Id;
             
        }

        public int GetDepartmentID(string _DepartmentGuid)
        {
            Guid _Departmentguid = Guid.Parse(_DepartmentGuid);
            var Departmentid = appDbContext.Departments.FirstOrDefault(g => g.DepartmentGuid == _Departmentguid);
            return Departmentid.Id;

        }
    }
}

