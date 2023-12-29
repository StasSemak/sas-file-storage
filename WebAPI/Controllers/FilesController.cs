﻿using BussinessLogic.DTOs;
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
        public async Task<IActionResult> Upload([FromBody] UploadFileDto model)
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

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteFileDto model)
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

        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetUploadById([FromRoute] string id, [FromQuery] string key)
        {
            try
            {
                var info = await service.GetUploadByIdAsync(id, key);
                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("info/{name}")]
        public async Task<IActionResult> GetUploadByName([FromRoute] string name, [FromQuery] string key)
        {
            try
            {
                var info = await service.GetUploadByNameAsync(name, key);
                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("uploads")]
        public async Task<IActionResult> GetAllUploads([FromQuery] string key)
        {
            try
            {
                var uploads = await service.GetAllUploadsAsync(key);
                return Ok(uploads);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("uploads/{id}")]
        public async Task<IActionResult> GetUploadsByUser([FromRoute]string id, [FromQuery] string key)
        {
            try
            {
                var uploads = await service.GetUploadsByUserAsync(id, key);
                return Ok(uploads);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("uploads/{type}")]
        public async Task<IActionResult> GetUploadsByType([FromRoute]string type, [FromQuery] string key)
        {
            try
            {
                var uploads = await service.GetUploadsByMimeAsync(type, key);
                return Ok(uploads);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
