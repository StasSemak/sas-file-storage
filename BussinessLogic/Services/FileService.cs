using BussinessLogic.DTOs;
using BussinessLogic.Interfaces;
using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public class FileService : IFileService
    {
        private readonly AppDbContext context;

        public FileService(AppDbContext context)
        {
            this.context = context;
        }

        public Task DeleteFile(DeleteFileDto model)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadFile(UploadFileDto model)
        {
            throw new NotImplementedException();
        }
    }
}
