using CoreApi.DatabaseModels;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderMasterController : ControllerBase
    {

        private readonly IGenderMaster _IGenderMaster;
        private readonly ILogger<GenderMasterController> logger;
        public GenderMasterController(IGenderMaster IGenderMaster  ,ILogger<GenderMasterController> logger)
        {
            this._IGenderMaster = IGenderMaster;
            this.logger = logger;
        }

        #region  Get all GenderList 
     
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _IGenderMaster.GetGenderList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database...");
            } 
        }
        #endregion


        #region
        [HttpPost]
        public async Task<IActionResult> AddGender([FromBody] GenderModel genderModel)
        {
            try
            {
                if (genderModel == null || string.IsNullOrEmpty(genderModel.Name))
                {
                    return BadRequest("Invalid gender data. The gender name is missing.");
                }
                var addedGender = await _IGenderMaster.AddGender(genderModel);
                return Ok(addedGender);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a new gender.");
                return StatusCode(500, "An error occurred while adding a new gender.");
            }
        }
        #endregion
    }
}
