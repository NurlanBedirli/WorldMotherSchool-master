using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace WorldMotherSchool.Areas.momsch.Core
{
    public class ImagesFile : IImageEnviroment
    {
        public async Task<string> CreateImageAsync(IFormFile Photo, IHostingEnvironment hosting, IFileNameGenerator nameGenerator)
        {
            string fileNames = "";

            if (Photo != null)
            {
                    if(FileExistansion.IsPhotoFileFormat(Photo))
                    {
                        var fullpath = Path.Combine(hosting.WebRootPath, "img");
                        var format = FileExistansion.GetFileFormat(Photo);
                        var newFileName = nameGenerator.GetFileName(format);
                        var filePath = Path.Combine(fullpath, newFileName);
                        await Photo.SaveFileAsync(filePath);
                        fileNames = newFileName;
                    }
            }
            return fileNames;
        }


        public bool DeleteImagePath(string Photo, IHostingEnvironment hosting)
        {
            bool delete = false;
            if (string.IsNullOrEmpty(Photo))
            {
                delete = false;
            }
            else
            {
                var filePath = Path.Combine(hosting.WebRootPath, "img",Photo);
                if(File.Exists(filePath))
                {
                    File.Delete(filePath);
                    delete = true;
                }
            }
            return delete;
        }

    }
}
