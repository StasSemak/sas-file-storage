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
        Task<string> UploadFile(UploadFileDto model);
        Task DeleteFile(DeleteFileDto model);
    }
}
