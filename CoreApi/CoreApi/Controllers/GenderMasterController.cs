using CoreApi.DatabaseModels;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

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


        #region  Add for gender in table 
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

        #region  for get gender by guid 
        [HttpGet("{genderGuid}")]
        public async Task<IActionResult> GetGenderByGuid(string genderGuid)
        {
            try
            {
                var gender = await _IGenderMaster.GetGenderByGuid(genderGuid);

                if (gender == null)
                {
                    return NotFound("Gender not found.");
                }

                return Ok(gender);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching gender by Guid.");
                return StatusCode(500, "An error occurred while fetching gender by Guid.");
            }
        }
        #endregion

        #region for gender update 

        [HttpPut("{genderGuid}")]
        public async Task<IActionResult> UpdateGender(string genderGuid, [FromBody] GenderModel updatedGender)
        {
            try
            {
                // Validate the input data
                if (string.IsNullOrEmpty(genderGuid) || updatedGender == null)
                {
                    return BadRequest("Invalid input data.");
                }

                // Set the GenderGuid from the URL parameter into the updatedGender model
                updatedGender.GenderGuid = genderGuid;

                // Call the UpdateGender method from the GenderService
                await _IGenderMaster.UpdateGender(updatedGender);

                return Ok("Gender updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating the gender.");
                return StatusCode(500, "An error occurred while updating the gender.");
            }
        }
        #endregion

    }
}
