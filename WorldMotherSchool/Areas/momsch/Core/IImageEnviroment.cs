using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Areas.momsch.Core
{
   public interface IImageEnviroment
    {
        bool DeleteImagePath(string Photo, IHostingEnvironment hosting);
        Task<string> CreateImageAsync(IFormFile Photo, IHostingEnvironment hosting,IFileNameGenerator nameGenerator);
    }
}
