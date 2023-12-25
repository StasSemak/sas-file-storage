using BussinessLogic.DTOs;
using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService service;

        public FilesController(IFileService service)
        {
            this.service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromBody]UploadFileDto model)
        {
            try
            {
                var res = await service.UploadFileAsync(model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody]DeleteFileDto model)
        {
            try
            {
                await service.DeleteFileAsync(model);
                return Ok();    
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
