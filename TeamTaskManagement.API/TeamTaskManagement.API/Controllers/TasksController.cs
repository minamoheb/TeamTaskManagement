using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManagement.Services.Dtos;
using TeamTaskManagement.Services.Services;

namespace TeamTaskManagement.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TasksController> _logger;
        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }
        #region API
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] TaskModel model)
        {
            try
            {
                var retVal = await _taskService.Insert(model).ConfigureAwait(false);
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Guid.NewGuid().ToString(), "api/Task/Create");
                return BadRequest(new { errors = ex.Message.Split("; ") });
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Edit([FromBody] TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var retdata = await _taskService.Edit(model).ConfigureAwait(false);
                return Ok(retdata);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Guid.NewGuid().ToString(), "api/Task/Edit");
                return BadRequest(new { errors = ex.Message.Split("; ") });
            }

        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<ActionResult> Delete([FromQuery] long id)
        {
            try
            {
                await _taskService.Delete(id).ConfigureAwait(false);
                return Ok();
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, Guid.NewGuid().ToString(), "api/Task/Delete");
                return BadRequest(new { errors = ex.Message.Split("; ") });
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<ResponseBase<IEnumerable<TaskModel>>>>> GetTaskData()
        {
            try
            {
                var retVal = new ResponseBase<IEnumerable<TaskModel>>();
                retVal = await _taskService.GetAll().ConfigureAwait(false);
                if (retVal == null)
                {
                    _logger.LogWarning("Msg:No Data Found" + "/Path:" + "api/Task/GetTaskData", Guid.NewGuid().ToString());
                    return NotFound("No Data found.");
                }
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Guid.NewGuid().ToString(), "api/Task/GetTaskData");
                return BadRequest(ex);
                //_logger.LogError(ex, "Error retrieving item {ItemId}", id);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<ResponseBase<TaskModel>>> GetTaskDataById([FromQuery] long id)
        {
            try
            {
                var retVal = new ResponseBase<TaskModel>();
                retVal = await _taskService.GetById(id).ConfigureAwait(false);
                if (retVal == null)
                {
                    _logger.LogWarning("Msg:No Data Found" + "/Path:" + "api/Task/GetTaskDataById", Guid.NewGuid().ToString());
                    return NotFound("No Data found.");
                }
                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Guid.NewGuid().ToString(), "api/Task/GetTaskDataById");
                return BadRequest(ex);
                //_logger.LogError(ex, "Error retrieving item {ItemId}", id);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> FilterData([FromQuery] string? name, int? status = 0)
        {
            try
            {
                var data = await _taskService.FilterData(status, name).ConfigureAwait(false);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Guid.NewGuid().ToString(), "api/Task/GetTaskData");
                return BadRequest(ex);
            }
        }




        #endregion
    }
}
