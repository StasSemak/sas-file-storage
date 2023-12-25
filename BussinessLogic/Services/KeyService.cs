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
            if(Constants.IsDevelopment)
            {
                keysFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).FullName, "Keys");
            }
            else keysFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Keys");
        }

        public async Task<bool> IsSecurityKeyValidAsync(string key)
        {
            StreamReader reader = new StreamReader(Path.Combine(keysFolderPath, "SecurityKeyHash.txt"));
            string storedKey = await reader.ReadToEndAsync();
            string passedKey = Hasher.GetHashString(key);

            return passedKey == storedKey;
        }

        public async Task<bool> IsAdminKeyValidAsync(string key)
        {
            StreamReader reader = new StreamReader(Path.Combine(keysFolderPath, "AdminKeyHash.txt"));
            string storedKey = await reader.ReadToEndAsync();
            string passedKey = Hasher.GetHashString(key);

            return passedKey == storedKey;
        }
    }
}
