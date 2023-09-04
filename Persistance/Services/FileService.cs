using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Persistance.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DeleteImage(string path, string folderName)
        {
            var imagePath = Path.Combine(_configuration.GetSection("Images:" + folderName).Value, path);
            try
            {
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            catch(Exception ex)
            {
            }
            
        }

        public string GetImage(string path, string folderName)
        {
            var imagePath = Path.Combine(_configuration.GetSection("Images:" + folderName).Value,path);
            string? extension = Path.GetExtension(path)?.TrimStart('.');
            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                var base64String = Convert.ToBase64String(imageBytes);
                return $"data:image/{extension};base64," + base64String;
            }
            else
            {
                return "";
            }
        }

        public async Task<string> UploadImage(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(_configuration.GetSection("Images:" + folderName).Value, uniqueFileName);

                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return uniqueFileName; // Return the path to the saved file
            }
            catch (Exception ex)
            {
                throw new Exception($"File upload failed: {ex.Message}");
            }
        }
    }
}
