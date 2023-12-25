﻿using BussinessLogic.DTOs;
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
    }
}
