using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Helpers
{
    public class FilesWorker
    {
        private readonly string filesFolder;
        public FilesWorker()
        {
            filesFolder = Constants.FilesFolderPath;
        }

        public async Task<string> SaveFileAsync(string filename, string base64)
        {
            if (string.IsNullOrWhiteSpace(base64)) throw new Exception("Unable to convert empty string!");

            string fileExtension = filename.Split('.', StringSplitOptions.RemoveEmptyEntries).Last();
            string base64Prefix = base64.Split(',')[0];
            string savedFileName = Path.GetRandomFileName() + $".{fileExtension}";

            if(base64Prefix.Contains("image"))
            {
                try
                {
                    await ImageWorker.SaveImageAsync(base64, filesFolder, savedFileName, fileExtension);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error saving image {savedFileName}! {ex.Message}");
                }
            }
            else
            {
                try
                {
                    string filePath = Path.Combine(filesFolder, savedFileName);
                    if (base64.Contains(',')) base64 = base64.Split(',')[1];

                    byte[] bytes = Convert.FromBase64String(base64);
                    await File.WriteAllBytesAsync(filePath, bytes);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error saving file {savedFileName}! {ex.Message}");
                }
            }

            return savedFileName;
        }

        public async Task RemoveFileAsync(string filename)
        {
            return Task.Run(() =>
            {
                try
                {
                    string file = Path.Combine(filesFolder, filename);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                catch
                {
                    throw new Exception($"Error removing file {filename}!");
                }
            });
        }
    }
}
