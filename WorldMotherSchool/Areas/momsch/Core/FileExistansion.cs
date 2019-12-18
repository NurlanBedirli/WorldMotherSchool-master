using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Areas.momsch.Core
{
    public static class FileExistansion
    {
        public static bool IsPhotoFileFormat(this IFormFile Photo)
        {
            return Photo.ContentType == "image/jpeg" || Photo.ContentType == "image/png";
        }

        public static string GetFileFormat(this IFormFile Photo)
        {
            return Photo.FileName.Substring(Photo.FileName.LastIndexOf(".") + 1, 3);
        }

        public static async Task SaveFileAsync(this IFormFile Photo,string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await Photo.CopyToAsync(stream);
            }
        }
    }
}
