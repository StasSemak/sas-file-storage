using BussinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(UploadFileDto model);
        Task DeleteFileAsync(DeleteFileDto model);
        Task<UploadDto> GetUploadByIdAsync(string id, string key);
        Task<UploadDto> GetUploadByNameAsync(string name, string key);
        Task<ICollection<UploadDto>> GetAllUploadsAsync(string key);
        Task<ICollection<UploadDto>> GetUploadsByUserAsync(string userId, string key);
        Task<ICollection<UploadDto>> GetUploadsByMimeAsync(string type, string key);
    }
}
