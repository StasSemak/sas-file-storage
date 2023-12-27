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
            if (Constants.IsDevelopment)
            {
                logFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).FullName, "logs.txt");
            }
            else logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
        }

        public async Task LogSuccessAsync(string userId, string fileName, bool isUpload = true)
        { 
            StreamWriter writer = new StreamWriter(logFilePath);
            string message = $"{DateTime.UtcNow}\tFile {fileName} is {(isUpload ? "uploaded" : "deleted")} by user {userId}.";
            await writer.WriteLineAsync(message);
            writer.Close();
        }

        public async Task LogErrorAsync(string userId, string errorMessage)
        {
            StreamWriter writer = new StreamWriter(logFilePath);
            string message = $"{DateTime.UtcNow}\tERROR! {errorMessage}. User: {userId}.";
            await writer.WriteLineAsync(message);
            writer.Close();
        }
    }
}
