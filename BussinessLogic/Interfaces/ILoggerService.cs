using BussinessLogic.DTOs;
using BussinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface ILoggerService
    {
        Task LogSuccessAsync(string userId, string fileName, bool isUpload = true);
        Task LogErrorAsync(string userId, string errorMessage);
        Task<ICollection<LogDto>> GetLogsAsync(string key, LogsDurationOptions options = LogsDurationOptions.None);
    }
}
