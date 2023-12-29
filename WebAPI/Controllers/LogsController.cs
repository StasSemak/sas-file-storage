using BussinessLogic.Enums;
using BussinessLogic.Exceptions;
using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILoggerService service;

        public LogsController(ILoggerService service)
        {
            this.service = service;
        }

        [HttpGet("{options}")]
        public async Task<IActionResult> GetLogs([FromRoute]LogsDurationOptions options, [FromQuery]string key)
        {
            try
            {
                var logs = await service.GetLogsAsync(key, options);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedException) return Unauthorized(new { error = "Unauthorized" });
                if (ex is InternalServerException) return StatusCode(500, new { error = "Internal error" });
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
