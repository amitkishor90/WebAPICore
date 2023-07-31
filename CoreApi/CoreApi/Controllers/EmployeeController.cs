using CoreApi.ApiResponse;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
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
    }
}
