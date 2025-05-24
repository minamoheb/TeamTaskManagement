using Microsoft.AspNetCore.Mvc;
using TeamTaskManagement.Services.Dtos;
using TeamTaskManagement.Services.Helper;
using TeamTaskManagement.Services.Services;
namespace TeamTaskManagement.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LookupItemsController : ControllerBase
    {
        private readonly ILookupItemsService _Service;
        private readonly ILogger<LookupItemsController> _logger;
        public LookupItemsController(ILookupItemsService Service, ILogger<LookupItemsController> logger)
        {
            _Service = Service;
            _logger = logger;
        }
        #region API

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<ResponseBase<IEnumerable<LookupItemsModel>>>> GetPriority()
        {
            try
            {
                var retVal = new ResponseBase<IEnumerable<LookupItemsModel>>();
                retVal = await _Service.GetAllByLookupId((long)LookupEnum.Priority).ConfigureAwait(false);
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Guid.NewGuid().ToString(), "api/GetPriority");
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<LookupItemsModel>>> GetStatus()
        {
            try
            {
                var retVal = new ResponseBase<IEnumerable<LookupItemsModel>>();
                retVal = await _Service.GetAllByLookupId((long)LookupEnum.Status).ConfigureAwait(false);
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Guid.NewGuid().ToString(), "api/GetStatus");
                return BadRequest(ex);
            }
        }


        #endregion
    }
}
