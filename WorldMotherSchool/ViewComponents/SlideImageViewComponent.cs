using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldMotherSchool.Language;
using WorldMotherSchool.Models;

namespace WorldMotherSchool.ViewComponents
{
    [ViewComponent(Name ="SlideFigures")]
    public class SlideImageViewComponent : ViewComponent
    {
        private SchoolDbContext dbContext { get; set; }

        public SlideImageViewComponent(SchoolDbContext _dbContext)
        { 
            dbContext = _dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            var lang = request.RequestCulture.UICulture.Name;
            var culture = SupportedLanguage.GetUILanguage(lang);

            int? cultureId = await dbContext.Languages.Where(x => x.Culture == culture).Select(x => x.Id).FirstAsync();
            var slideFigures =  await dbContext.SlideFigures.Where(x => x.LanguageId == (int)cultureId).ToListAsync();
            
            return View(slideFigures);
        }
     
    }
}
