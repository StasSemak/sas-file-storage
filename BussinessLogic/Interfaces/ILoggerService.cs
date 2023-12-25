using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface ILoggerService
    {
        Task LogSuccessAsync(string userId, string fileName);
        Task LogErrorAsync(string userId, string errorMessage);
    }
}
