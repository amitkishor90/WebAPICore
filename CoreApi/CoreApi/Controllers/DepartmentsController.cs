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
    }
}
