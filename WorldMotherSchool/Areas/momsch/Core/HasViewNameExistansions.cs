using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldMotherSchool.Models;

namespace WorldMotherSchool.Areas.momsch.Core
{
    public static class HasViewNameExistansions
    {
        public static bool  IsSuchName(this SchoolDbContext dbContext, int? value)
        {
            bool isFound = false;
            if(value.HasValue)
            {
                var viewName = dbContext.ResourcesViews.Where(x => x.Id == value).FirstOrDefault();
                if(viewName != null)
                {
                    isFound = true;
                }
            }
            return isFound;
        }

        public static bool IsViewIdOrLanguageId(this SchoolDbContext dbContext,int? viewId,int? languageId)
        {
            bool isFound = false;
           var data = dbContext.MainContents.Where(x => x.ResourcesViewId == viewId).ToList();
            foreach(var elm in data)
            {
                if(elm.LanguageId == languageId)
                {
                    isFound = true;
                }
            }
            return isFound;
        }

        public static string GetViewName(this SchoolDbContext dbContext, int? viewId)
        {
            var view = dbContext.ResourcesViews.Where(x => x.Id == viewId).FirstOrDefault();
            string ViewName = "";
            if (view != null)
            {
                ViewName = view.Name;
            }
            return ViewName;
        }

        public static async Task AddElement(this SchoolDbContext dbContext, int? viewId, int? languageId,string text)
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                MainContent mainContent = new MainContent
                {
                    LanguageId = (int)languageId,
                    ResourcesViewId = (int)viewId,
                    Text = text
                };
                await dbContext.MainContents.AddAsync(mainContent);
                await dbContext.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public static async Task RemovePathPhotos(this SchoolDbContext dbContext,IImageEnviroment imageEnviroment,IHostingEnvironment hostingEnvironment, string[] photo)
        {
            if(photo.Length != 0)
            {
                foreach (string elm in photo)
                {
                    imageEnviroment.DeleteImagePath(elm, hostingEnvironment);
                    await Task.Delay(0);
                }
            }
        }
    }
}
