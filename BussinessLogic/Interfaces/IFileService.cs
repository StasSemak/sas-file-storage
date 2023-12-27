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
        Task<FileInfoDto> GetFileInfoByIdAsync(string id, string key);
        Task<FileInfoDto> GetFileInfoByNameAsync(string name, string key);
        Task<ICollection<FileInfoDto>> GetAllAsync(string key);
        Task<ICollection<FileInfoDto>> GetByUserAsync(string userId, string key);
        Task<ICollection<FileInfoDto>> GetByMimeAsync(string type, string key);
    }
}
