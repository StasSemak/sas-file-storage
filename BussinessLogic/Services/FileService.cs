using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.Helpers;
using BussinessLogic.Interfaces;
using DataLayer.Data;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper mapper;

        public FileService(AppDbContext context, IKeyService keyService, ILoggerService loggerService, 
            FilesWorker filesWorker, IMapper mapper)
        {
            this.context = context;
            this.keyService = keyService;
            this.loggerService = loggerService;
            this.filesWorker = filesWorker;
            this.mapper = mapper;
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

        public async Task DeleteFileAsync(DeleteFileDto model)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(model.SecurityKey))
                {
                    if(!await keyService.IsSecurityKeyValidAsync(model.SecurityKey)) throw new UnauthorizedAccessException();

                    var storage = await context.Storages
                        .Where(x => x.UserId == model.UserId)
                        .Where(x => x.FileName == model.FileName)
                        .SingleOrDefaultAsync();
                    if (storage == null) throw new Exception($"File ${model.FileName} not found!");

                    await filesWorker.RemoveFileAsync(model.FileName);
                    context.Storages.Remove(storage);
                    await context.SaveChangesAsync();

                    await loggerService.LogSuccessAsync(model.UserId, model.FileName, false);
                }
                else if(!string.IsNullOrWhiteSpace(model.AdminKey)) 
                {
                    if (!await keyService.IsAdminKeyValidAsync(model.AdminKey)) throw new UnauthorizedAccessException();

                    var storage = await context.Storages
                        .Where(x => x.FileName == model.FileName)
                        .SingleOrDefaultAsync();
                    if (storage == null) throw new Exception($"File ${model.FileName} not found!");

                    await filesWorker.RemoveFileAsync(model.FileName);
                    context.Storages.Remove(storage);
                    await context.SaveChangesAsync();

                    await loggerService.LogSuccessAsync("ADMIN", model.FileName, false);
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (Exception ex)
            {
                await loggerService.LogErrorAsync((string.IsNullOrWhiteSpace(model.AdminKey) ? model.UserId : "ADMIN"), ex.Message);
                throw;
            }
        }

        public async Task<FileInfoDto> GetFileInfoByIdAsync(string id, string key)
        {
            if(!await keyService.IsAdminKeyValidAsync(key)) throw new UnauthorizedAccessException();

            var storage = await context.Storages.FindAsync(id);
            if (storage == null) throw new Exception($"Not found file with id ${id}!");

            return new FileInfoDto
            {
                Id = storage.Id,
                FileName = storage.FileName,
                CreatedAt = storage.CreatedAt,
                MimeType = storage.MimeType,
                UserId = storage.UserId
            };
        }

        public async Task<FileInfoDto> GetFileInfoByNameAsync(string name, string key)
        {
            if (!await keyService.IsAdminKeyValidAsync(key)) throw new UnauthorizedAccessException();

            var storage = await context.Storages
                .Where(x => x.FileName == name)
                .SingleOrDefaultAsync();
            if (storage == null) throw new Exception($"Not found file with name ${name}!");

            return mapper.Map<FileInfoDto>(storage);   
        }

        public async Task<ICollection<FileInfoDto>> GetAllAsync(string key)
        {
            if (!await keyService.IsAdminKeyValidAsync(key)) throw new UnauthorizedAccessException();

            var storages = await context.Storages.ToListAsync();

            return mapper.Map<ICollection<FileInfoDto>>(storages);
        }

        public async Task<ICollection<FileInfoDto>> GetByUserAsync(string userId, string key)
        {
            if (!await keyService.IsAdminKeyValidAsync(key)) throw new UnauthorizedAccessException();

            var storages = await context.Storages
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return mapper.Map<ICollection<FileInfoDto>>(storages);
        }

        public async Task<ICollection<FileInfoDto>> GetByMimeAsync(string type, string key)
        {
            if (!await keyService.IsAdminKeyValidAsync(key)) throw new UnauthorizedAccessException();

            var storages = await context.Storages
                .Where(x => x.MimeType == type)
                .ToListAsync();

            return mapper.Map<ICollection<FileInfoDto>>(storages);
        }
    }
}
