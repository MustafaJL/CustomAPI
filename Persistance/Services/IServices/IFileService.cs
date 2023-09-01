using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Services.IServices
{
    public interface IFileService
    {
        string GetImage(string path, string folderName);
        Task<string> UploadImage(IFormFile file, string folderName);
    }
}
