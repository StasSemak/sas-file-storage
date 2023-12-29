using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface IStorageService
    {
        Task<string> SaveFileAsync(string filename, string base64);
        Task RemoveFileAsync(string filename);
    }
}
