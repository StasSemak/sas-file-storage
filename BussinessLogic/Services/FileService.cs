using BussinessLogic.DTOs;
using BussinessLogic.Helpers;
using BussinessLogic.Interfaces;
using DataLayer.Data;
using DataLayer.Models;
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
        private readonly IKeyService keyService;
        private readonly ILoggerService loggerService;
        private readonly FilesWorker filesWorker;

        public FileService(AppDbContext context, IKeyService keyService, ILoggerService loggerService, FilesWorker filesWorker)
        {
            this.context = context;
            this.keyService = keyService;
            this.loggerService = loggerService;
            this.filesWorker = filesWorker;
        }

        public async Task<string> UploadFileAsync(UploadFileDto model)
        {
            try
            {
                if(!await keyService.IsSecurityKeyValidAsync(model.SecurityKey))
                {
                    throw new UnauthorizedAccessException();
                }

                string filename = await filesWorker.SaveFileAsync(model.FileName, model.Base64);

                var storage = new Storage();
                storage.FileName = filename;
                storage.UserId = model.UserId;
                storage.MimeType = model.FileName.Split('.', StringSplitOptions.RemoveEmptyEntries).Last();

                await context.Storages.AddAsync(storage);

                await loggerService.LogSuccessAsync(model.UserId, filename);

                return filename;
            }
            catch (Exception ex)
            {
                await loggerService.LogErrorAsync(model.UserId, ex.Message);
                throw;
            }
        }

        public Task DeleteFileAsync(DeleteFileDto model)
        {
            throw new NotImplementedException();
        }
    }
}
