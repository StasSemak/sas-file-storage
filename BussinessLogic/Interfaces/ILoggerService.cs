using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface ILoggerService
    {
        Task LogSuccess(string userId, string fileName);
        Task LogError(string userId, string errorMessage);
    }
}
