using BussinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly string logFilePath;
        public LoggerService() 
        {
            logFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).FullName, "Logs.log");
        }

        public async Task LogSuccess(string userId, string fileName)
        { 
            StreamWriter writer = new StreamWriter(logFilePath);
            string message = $"{DateTime.UtcNow}\tFile {fileName} is uploaded by user {userId}.";
            await writer.WriteLineAsync(message);
            writer.Close();
        }

        public async Task LogError(string userId, string errorMessage)
        {
            StreamWriter writer = new StreamWriter(logFilePath);
            string message = $"{DateTime.UtcNow}\tERROR! {errorMessage}. User: {userId}.";
            await writer.WriteLineAsync(message);
            writer.Close();
        }
    }
}
