using BussinessLogic.Helpers;
using BussinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public class KeyService : IKeyService
    {
        private readonly string keysFolderPath;

        public KeyService() 
        {
            keysFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).FullName, "Keys");
        }

        public async Task<bool> IsSecurityKeyValid(string key)
        {
            StreamReader reader = new StreamReader(Path.Combine(keysFolderPath, "SecurityKeyHash.txt"));
            string storedKey = await reader.ReadToEndAsync();
            string passedKey = Hasher.GetHashString(key);

            return passedKey == storedKey;
        }

        public async Task<bool> IsAdminKeyValid(string key)
        {
            StreamReader reader = new StreamReader(Path.Combine(keysFolderPath, "AdminKeyHash.txt"));
            string storedKey = await reader.ReadToEndAsync();
            string passedKey = Hasher.GetHashString(key);

            return passedKey == storedKey;
        }
    }
}
