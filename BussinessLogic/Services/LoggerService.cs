using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.Enums;
using BussinessLogic.Exceptions;
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
    public class LoggerService : ILoggerService
    {
        private readonly AppDbContext context;
        private readonly IKeyService keyService;
        private readonly IMapper mapper;

        public LoggerService(AppDbContext context, IKeyService keyService, IMapper mapper) 
        {
            this.context = context;
            this.keyService = keyService;
            this.mapper = mapper;
        }

        public async Task LogSuccessAsync(string userId, string fileName, bool isUpload = true)
        {
            var log = new Log()
            {
                Message = $"File {fileName} is {(isUpload ? "uploaded" : "deleted")} by user {userId}.",
                Status = "SUCCESS"
            };
            await context.Logs.AddAsync(log);
            await context.SaveChangesAsync();
        }

        public async Task LogErrorAsync(string userId, string errorMessage)
        {
            var log = new Log()
            {
                Message = $"ERROR! {errorMessage}. User: {userId}.",
                Status = "ERROR"
            };
            await context.Logs.AddAsync(log);
            await context.SaveChangesAsync();
        }

        public async Task<ICollection<LogDto>> GetLogsAsync(string key, LogsDurationOptions options = LogsDurationOptions.None)
        {
            if (!await keyService.IsAdminKeyValidAsync(key)) throw new UnauthorizedException();

            var logs = await context.Logs
                .Where(x => x.Date > GetDateFromOption(options))
                .ToListAsync();

            return mapper.Map<ICollection<LogDto>>(logs);
        }

        private DateTime GetDateFromOption(LogsDurationOptions options)
        {
            if (options == LogsDurationOptions.Year) return DateTime.UtcNow - TimeSpan.FromDays(365);
            if (options == LogsDurationOptions.Month) return DateTime.UtcNow - TimeSpan.FromDays(30);
            if (options == LogsDurationOptions.Week) return DateTime.UtcNow - TimeSpan.FromDays(7);
            if (options == LogsDurationOptions.Day) return DateTime.UtcNow - TimeSpan.FromDays(1);
            return DateTime.MinValue;
        }
    }
}
