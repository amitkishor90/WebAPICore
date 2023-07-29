using CoreApi.DatabaseModels;
using CoreApi.InterfacesWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderMasterController : ControllerBase
    {

        private readonly IGenderMaster _IGenderMaster;
        public GenderMasterController(IGenderMaster IGenderMaster)
        {
            this._IGenderMaster = IGenderMaster;
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
    }
}
