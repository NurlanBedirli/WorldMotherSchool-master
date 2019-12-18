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
    [ViewComponent(Name ="EventsAbout")]
    public class EventAboutViewComponent : ViewComponent
    {
        private SchoolDbContext dbContext;
        public EventAboutViewComponent( SchoolDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            var lang = request.RequestCulture.UICulture.Name;
            var culture = SupportedLanguage.GetUILanguage(lang);
            List<EventsAboutModel> eventsAboutModels = new List<EventsAboutModel>();
            try
            {
                var cultureId = dbContext.Languages
                                 .Where(x => x.Culture == culture)
                                           .First()
                                                  .Id;

                eventsAboutModels = await dbContext.EventAbouts
                                     .OrderByDescending(x=> x.Id)
                                                .Include("EventAboutLanguage")
                                                          .Include("EventAboutPhoto")
                                                                     .Select(x => new EventsAboutModel
                {
                    EventId = x.Id,
                    EventAboutLanguages = x.EventAboutLanguages.Where(y => y.LanguageId == cultureId).ToList(),
                    DateTime = x.DateTime,
                    EventAboutPhotos = x.EventAboutPhotos.Where(z => z.EventAboutId == x.Id).ToList()
                }).ToListAsync();
            }
            catch(Exception exp)
            {
               ModelState.AddModelError("", exp.Message);
            }
            return View(eventsAboutModels);
        }
    }
}
