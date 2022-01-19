using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Studentski_hotel.Helper
{
    public class Image
    {
        public static string Upload(IFormFile image, IWebHostEnvironment webhost, string modelName)
        {

            string uniqueFileName = null;

            if (image != null)
            {
                string uploadsFolder = Path.Combine(webhost.WebRootPath, "images", modelName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public static void Delete(IWebHostEnvironment webhost, string folder, string fileName)
        {
            string fullPath = Path.Combine(webhost.WebRootPath, "images", folder, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}

