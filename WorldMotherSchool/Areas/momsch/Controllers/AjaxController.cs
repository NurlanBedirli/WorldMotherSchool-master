using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldMotherSchool.Areas.momsch.Core;
using WorldMotherSchool.Models;

namespace WorldMotherSchool.Areas.momsch.Controllers
{
    [Area("momsch")]
    public class AjaxController : Controller
    {
        private SchoolDbContext schoolDb;
        private IHostingEnvironment hosting;
        private IFileNameGenerator nameGenerator;
        private IImageEnviroment imageEnviroment;

        public AjaxController(SchoolDbContext _schoolDb,
                              IHostingEnvironment _environment,
                              IFileNameGenerator _nameGenerator,
                              IImageEnviroment _imageEnviroment)
        {
            schoolDb = _schoolDb;
            hosting = _environment;
            nameGenerator = _nameGenerator;
            imageEnviroment = _imageEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ResourcesViewAjax()
        {
            var data =  await schoolDb.ResourcesViews.ToListAsync();
            if(data != null)
            {
                return Json(new {data = data, message = 200 });
            }else
            {
                return Json(new {data = data ,message = 400 });
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetRoleAjax()
        {
            var data = await schoolDb.Roles.ToListAsync();
            await Task.Delay(0);
            return Json(new { message = 200,role = data });
        }

        [HttpPost]
        public JsonResult DeletePhotoPath(string photo)
        {
            if(photo != null)
            {
                if(imageEnviroment.DeleteImagePath(photo, hosting))
                {
                    return Json(new { message = 200});
                }
            }
            return Json(new {message = 400 });
        }

        [HttpPost]
        public async Task<JsonResult> DeletePhotoDataBase(string photo)
        {
            if (photo != null)
            {
                if (imageEnviroment.DeleteImagePath(photo, hosting))
                {
                   var data = await schoolDb.Photos.Where(x => x.Image == photo).FirstOrDefaultAsync();
                    if(data!=null)
                    {
                    schoolDb.Photos.Remove(data);
                    await schoolDb.SaveChangesAsync();
                     return Json(new { message = 200 });
                    }
                }
            }
            return Json(new { message = 400 });
        }
    }
}