﻿using Azure;
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
        #region Add Employee 
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
        #endregion

        #region Delete employee by Guid 
        public async Task<ApiResponse<bool>> DeleteEmployeeAsync(string employeeGuid)
        {
            var response = new ApiResponse<bool>();
            try
            {
                if (Guid.TryParse(employeeGuid, out Guid guid))
                {
                    var employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Guid == guid);
                    if (employee != null)
                    {
                        appDbContext.Employees.Remove(employee);
                        await appDbContext.SaveChangesAsync();

                        response.Data = true;
                        response.Message = "Employee deleted successfully.";
                        response.IsError = false;
                        response.Status = "Success";
                    }
                    else
                    {
                        response.Message = "Employee not found.";
                        response.IsError = true;
                        response.Status = "Error";
                    }
                }
                else
                {
                    response.Message = "Invalid employee Guid.";
                    response.IsError = true;
                    response.Status = "Error";
                }
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred while deleting the employee.";
                response.IsError = true;
                response.Status = "Error";
                response.ExceptionMessage = ex.Message;
            }

            return response;
        }

        #endregion

        #region Get Employee with guid
        public async Task<ApiResponse<List<EmployeesModels>>> GetEmployeeAsync(string employeeGuid)
        {
            var response = new ApiResponse<List<EmployeesModels>>();
            try
            {
                if (Guid.TryParse(employeeGuid, out Guid guid))
                {
                    var employeesList = await (from employee in appDbContext.Employees
                                               join gender in appDbContext.Genders on employee.GenderId equals gender.Id
                                               join department in appDbContext.Departments on employee.DepartmentId equals department.Id
                                               select new EmployeesModels
                                               {
                                                   Guid = employee.Guid.ToString(),
                                                   FirstName = employee.FirstName,
                                                   LastName = employee.LastName,
                                                   Emailid = employee.Emailid,
                                                   PenCardNo = employee.PenCardNo,
                                                   Salary = employee.Salary,
                                                   GenderGuid = gender.GenderGuid.HasValue ? gender.GenderGuid.Value.ToString() : "B1424101-620C-47CA-92CD-F40A2B407D09",
                                                   DepartmentGuid = department.DepartmentGuid.HasValue ? department.DepartmentGuid.Value.ToString() : "3265D33D-CEB6-4E9B-9828-051FD371CBA5",
                                                   Address = employee.Address,
                                               }).ToListAsync();

                    response.Data = employeesList;
                    response.Message = "Employees found.";
                    response.IsError = false;
                    response.Status = "Success";
                }
                else
                {
                    response.Message = "Invalid employee Guid.";
                    response.IsError = true;
                    response.Status = "Error";
                }
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred while getting employees.";
                response.IsError = true;
                response.Status = "Error";
                response.ExceptionMessage = ex.Message;
            }

            return response;
        }
        #endregion

        #region  get all Employee Data 
        public async Task<ApiResponse<IEnumerable<EmployeesModelsList>>> GetEmployeesAsync()
        {
            var response = new ApiResponse<IEnumerable<EmployeesModelsList>>();
            try
            {
                // Simulate an asynchronous operation (e.g., querying a database) using Task.Delay
                // Replace this with actual asynchronous database access in a real application
                await Task.Delay(100);

                // Perform the join between Employees, Genders, and Departments
                var employeesList = (from employee in appDbContext.Employees
                                     join gender in appDbContext.Genders on employee.GenderId equals gender.Id
                                     join department in appDbContext.Departments on employee.DepartmentId equals department.Id
                                     select new EmployeesModelsList
                                     {
                                         EmployeeGuid = employee.Guid.ToString(),
                                         GenderGuid = gender.GenderGuid.ToString(),
                                         DepartmentGuid = department.DepartmentGuid.ToString(),
                                         FirstName = employee.FirstName,
                                         LastName = employee.LastName,
                                         Emailid = employee.Emailid,
                                         PenCardNo = employee.PenCardNo,
                                         salary = (double)Convert.ToDecimal(employee.Salary),
                                         GenderName = gender.Name,
                                         DepartmentName = department.Name,
                                         Address = employee.Address,
                                        
                                     }).ToList();

                response.Data = employeesList; // Assign the employeesList directly to the Data property of the ApiResponse
                response.Message = "Employees found.";
                response.IsError = false;
                response.Status = "Success";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting employees.");

                // Populate the response properties to indicate the error
                response.Data = null;
                response.Message = "Error while getting employees.";
                response.IsError = true;
                response.Status = "Error";

                // You can also add an additional property to store the actual exception message if needed.
                response.ExceptionMessage = ex.Message;
            }

            return response;
        }

        #endregion

        #region Update Employee Data 
        public async Task<ApiResponse<bool>> UpdateEmployeeAsync(EmployeesModels employee)
        {
            var response = new ApiResponse<bool>();
            int GenderID = GetGenderID(employee.GenderGuid);
            int DepartmentID = GetDepartmentID(employee.DepartmentGuid);
            try
            {
                if (Guid.TryParse(employee.Guid, out Guid guid))
                {
                    var existingEmployee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Guid == guid);
                    if (existingEmployee != null)
                    {
                        
                        // Update the employee properties
                        existingEmployee.FirstName = employee.FirstName;
                        existingEmployee.LastName = employee.LastName;
                        existingEmployee.Emailid = employee.Emailid;
                        existingEmployee.PenCardNo = employee.PenCardNo;
                        existingEmployee.Salary = employee.Salary;
                        existingEmployee.GenderId = GenderID;
                        existingEmployee.DepartmentId = DepartmentID;
                        existingEmployee.Address = employee.Address;

                        appDbContext.Employees.Update(existingEmployee);
                        await appDbContext.SaveChangesAsync();

                        response.Data = true;
                        response.Message = "Employee updated successfully.";
                        response.IsError = false;
                        response.Status = "Success";
                    }
                    else
                    {
                        response.Message = "Employee not found.";
                        response.IsError = true;
                        response.Status = "Error";
                    }
                }
                else
                {
                    response.Message = "Invalid employee Guid.";
                    response.IsError = true;
                    response.Status = "Error";
                }
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred while updating the employee.";
                response.IsError = true;
                response.Status = "Error";
                response.ExceptionMessage = ex.Message;
            }

            return response;
        }
        #endregion


        #region this method is get Gender Guid and department Guid
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

        #endregion
    }
}

