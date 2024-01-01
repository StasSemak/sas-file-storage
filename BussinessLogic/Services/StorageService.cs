using BussinessLogic.Exceptions;
using BussinessLogic.Helpers;
using BussinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public class StorageService : IStorageService
    {
        private readonly string filesFolder;
        public StorageService()
        {
            filesFolder = Constants.FilesFolderPath;
        }

        public async Task<string> SaveFileAsync(string filename, string base64)
        {
            if (string.IsNullOrWhiteSpace(base64)) throw new BadRequestException("Unable to save empty file!");

            string fileExtension = filename.Split('.', StringSplitOptions.RemoveEmptyEntries).Last();
            string base64Prefix = base64.Split(',')[0];        

            if (base64Prefix.Contains("image"))
            {
                string imageName = Path.GetRandomFileName() + $".{fileExtension}";
                try
                {
                    await ImageWorker.SaveImageAsync(base64, filesFolder, imageName, fileExtension);
                    return imageName;
                }
                catch (Exception ex)
                {
                    throw new InternalServerException($"Error saving image {imageName}! {ex.Message}");
                }
            }
            else
            {
                try
                {
                    string filePath = Path.Combine(filesFolder, filename);
                    if (base64.Contains(',')) base64 = base64.Split(',')[1];

                    byte[] bytes = Convert.FromBase64String(base64);
                    await File.WriteAllBytesAsync(filePath, bytes);
                    
                    return filename;
                }
                catch (Exception ex)
                {
                    throw new InternalServerException($"Error saving file {filename}! {ex.Message}");
                }
            }

            
        }

        public async Task RemoveFileAsync(string filename)
        {
            await Task.Run(() =>
            {
                try
                {
                    string file = Path.Combine(filesFolder, filename);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    else throw new BadRequestException($"File {filename} not exists!");
                }
                catch
                {
                    throw;
                }
            });
        }
    }
}
