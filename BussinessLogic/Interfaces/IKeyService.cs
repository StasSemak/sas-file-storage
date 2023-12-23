using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface IKeyService
    {
        Task<bool> IsSecurityKeyValid(string key);
        Task<bool> IsAdminKeyValid(string key);
    }
}
