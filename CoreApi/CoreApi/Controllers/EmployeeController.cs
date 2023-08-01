using CoreApi.ApiResponse;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreApi.Controllers
{
   
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeesMaster _IEmployeesMaster;
        private readonly ILogger<EmployeeController> logger;
        public EmployeeController(IEmployeesMaster IEmployeesMaster, ILogger<EmployeeController> logger)
        {
            this._IEmployeesMaster = IEmployeesMaster;
            this.logger = logger;
        }


        [HttpPost]
        [Route("api/AddEmployee")]
       
        public async Task<IActionResult> AddEmployee([FromBody] EmployeesModels employeeModel)
        {
            try
            {
                // Call the AddEmployeeAsync method from the employees repository
                var response = await _IEmployeesMaster.AddEmployeeAsync(employeeModel);

                // Check the IsError property of the response to determine if there was an error
                if (response.IsError)
                {
                    // Log the error using ILogger
                    logger.LogError("Error occurred while adding an employee: {ErrorMessage}", response.Message);
                    // Return the error response with the appropriate status code
                    return BadRequest(response);
                }

                // If successful, return a success response with the added employee
                return Ok(new ApiResponse<EmployeesModels>
                {
                    Data = response.Data,
                    Message = "Employee added successfully.",
                    Status = "success"
                });
            }
            catch (Exception ex)
            {
                // Log the exception using ILogger
                logger.LogError(ex, "An error occurred while adding an employee.");
                // Return the error response with a generic error message
                return StatusCode(500, new ApiResponse<EmployeesModels>
                {
                    IsError = true,
                    Message = "An error occurred while adding an employee."
                });
            }
        }
        [HttpGet]
        [Route("api/GetEmployee")]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmployeesModelsList>>>> GetEmployees()
        {
            // Call the GetEmployeesAsync method from the employeesRepository
            var response = await _IEmployeesMaster.GetEmployeesAsync();

            // Check if the ApiResponse indicates success or error
            if (!response.IsError)
            {
                return Ok(response); // Return 200 with the ApiResponse containing employees list
            }
            else
            {
                return BadRequest(response); // Return 400 with the ApiResponse containing error details
            }
        }

        [HttpGet("{employeeGuid}")]
        public async Task<ActionResult<ApiResponse<EmployeesModels>>> GetEmployee(string employeeGuid)
        {
            // Call the GetEmployeeAsync method from the employeesRepository
            var response = await _IEmployeesMaster.GetEmployeeAsync(employeeGuid);

            // Check if the ApiResponse indicates success or error
            if (!response.IsError)
            {
                return Ok(response); // Return 200 with the ApiResponse containing the employee data
            }
            else
            {
                return BadRequest(response); // Return 400 with the ApiResponse containing error details
            }
        }

        [HttpDelete("{employeeGuid}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteEmployee(string employeeGuid)
        {
            var response = await _IEmployeesMaster.DeleteEmployeeAsync(employeeGuid);
            if (response.IsError)
            {
                return BadRequest(response); // Or return any other appropriate error response
            }

            return Ok(response);
        }

        [HttpPut("api/EmployeeUpate")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateEmployee(EmployeesModels employee)
        {
            var response = await _IEmployeesMaster.UpdateEmployeeAsync(employee);
            if (response.IsError)
            {
                return BadRequest(response); // Or return any other appropriate error response
            }

            return Ok(response);
        }



    }
}
