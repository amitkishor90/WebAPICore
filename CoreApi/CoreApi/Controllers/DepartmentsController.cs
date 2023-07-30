using CoreApi.ApiResponse;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentMaster _IDepartmentMaster;
        private readonly ILogger<GenderMasterController> logger;
        public DepartmentsController(IDepartmentMaster IDepartmentMaster, ILogger<GenderMasterController> logger)
        {
            this._IDepartmentMaster = IDepartmentMaster;
            this.logger = logger;
        }
        #region
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentModel departmentModel)
        {
            try
            {
                // Validate the input data
                if (departmentModel == null)
                {
                    return BadRequest("Invalid department data.");
                }
                // Call the AddDepartmentAsync method from the DepartmentMaster service
                var addedDepartment = await _IDepartmentMaster.AddDepartmentAsync(departmentModel);
                return Ok(addedDepartment);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a new department.");
                return StatusCode(500, "An error occurred while adding a new department.");
            }
        }
        #endregion


        #region get department from guid 
        [HttpGet("{departmentGuid}")]
        public async Task<IActionResult> GetDepartment(string departmentGuid)
        {

            try
            {

                var response = await _IDepartmentMaster.GetDepartmentAsync(departmentGuid);
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching the department.");
                return StatusCode(500, "An error occurred while fetching the department.");
            }
        }
        #endregion

        #region
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var response = await _IDepartmentMaster.GetDepartmentsAsync();
                if (response.IsError)
                {
                    logger.LogError("Error occurred while fetching departments: {ErrorMessage}", response.Message);
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching departments.");
                return StatusCode(500, new ApiResponse<IEnumerable<DepartmentModel>>
                {
                    IsError = true,
                    Message = "An error occurred while fetching departments."
                });
            }
        }
        #endregion


        #region update Department 
        [HttpPut("{departmentGuid}")]
        public async Task<IActionResult> UpdateDepartment(string departmentGuid, [FromBody] DepartmentModel department)
        {
            try
            {
                // Check if the departmentGuid is a valid Guid format
                if (!Guid.TryParse(departmentGuid, out _))
                {
                    return BadRequest("Invalid departmentGuid format.");
                }

                // Update the department using the UpdateDepartmentAsync method from the department repository
                department.DepartmentGuid= departmentGuid;
                var response = await _IDepartmentMaster.UpdateDepartmentAsync(department);

                // Check the IsError property of the response to determine if there was an error
                if (response.IsError)
                {
                    // Log the error using ILogger
                    logger.LogError("Error occurred while updating the department: {ErrorMessage}", response.Message);
                    // Return the error response
                    return BadRequest(response);
                }

                // If successful, return the updated department data as Ok response
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception using ILogger
                logger.LogError(ex, "An error occurred while updating the department.");
                // Return the error response with a generic error message
                return StatusCode(500, new ApiResponse<DepartmentModel>
                {
                    IsError = true,
                    Message = "An error occurred while updating the department."
                });
            }
        }


        #endregion

        #region delete Department 
        [HttpDelete("{departmentGuid}")]
        public async Task<IActionResult> DeleteDepartment(string departmentGuid)
        {
            try
            {
                // Check if the departmentGuid is a valid Guid format
                if (!Guid.TryParse(departmentGuid, out _))
                {
                    return BadRequest("Invalid departmentGuid format.");
                }

                // Call the DeleteDepartmentAsync method from the department repository
                var response = await _IDepartmentMaster.DeleteDepartmentAsync(departmentGuid);

                // Check the IsError property of the response to determine if there was an error
                if (response.IsError)
                {
                    // Log the error using ILogger
                    logger.LogError("Error occurred while deleting the department: {ErrorMessage}", response.Message);
                    // Return the error response
                    return BadRequest(response);
                }

                // If successful, return a success response with a true value
                return Ok(new ApiResponse<bool>
                {
                    Data = response.Data,
                    Message = "Department deleted successfully.",
                    Status = "success"
                });
            }
            catch (Exception ex)
            {
                // Log the exception using ILogger
                logger.LogError(ex, "An error occurred while deleting the department.");
                // Return the error response with a generic error message
                return StatusCode(500, new ApiResponse<bool>
                {
                    IsError = true,
                    Message = "An error occurred while deleting the department."
                });
            }
        }


        #endregion
    }
}



