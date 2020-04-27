using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WorldMotherSchool.Language;
using WorldMotherSchool.Models;

namespace WorldMotherSchool.Controllers
{
    public class HomeController : Controller
    {
        private SchoolDbContext dbContext;
        private readonly IStringLocalizer<SharedResource> stringLocalizer;
        public readonly IStringLocalizer<HomeController> localizer;

        public HomeController(SchoolDbContext _dbContext,
               IStringLocalizer<SharedResource> _stringLocalizer,
                IStringLocalizer<HomeController> _localizer)
        {
            dbContext = _dbContext;
            stringLocalizer = _stringLocalizer;
            localizer = _localizer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [Route("/{culture}/home")]
        public IActionResult Index(string returnUrl)
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("/{culture}/about")]
        public ActionResult About()
        {
            ViewBag.Translate = localizer["Bizi Youtube Səhifəmizdən belə izləyə bilərsiniz"];
            return View();
        }

        [HttpGet]
        [Route("/{culture}/purpose")]
        public async Task<ActionResult> Purpose(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            ViewBag.Translate = localizer["Məqsədimiz"];
            try
            {
                string viewName = "Məqsədimiz";
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;
                    }
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/createschool")]
        public async Task<ActionResult> CreateSchool(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "Məktəbin Yaranması";
                ViewBag.Translate = localizer["Dünya Məktəbinin yaranması"];
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;
                    }
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/schoolhymn")]
        public async Task<ActionResult> SchoolHymn()
        {
            List<Photo> photos = new List<Photo>();
            try
            {
             string viewName = "SchoolHymn";
                ViewBag.Translate = localizer["Məktəbimizin Himni"];
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
             photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
            }
            catch(Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(photos);
        }

        [HttpGet]
        [Route("/{culture}/section_a_z")]
        public async Task<ActionResult> Azsection(string culture)
        {
                ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "AzBölməsi";
                ViewBag.Translate = localizer["Azərbaycan bölməsi"];
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;

                    }
                }
            }
            catch(Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
          
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/section_r_u")]
        public async Task<ActionResult> Rusection(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "RuBölməsi";
                ViewBag.Translate = localizer["Rus bölməsi"];
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;

                    }
                }
            }catch(Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
           
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/section_e_n")]
        public async Task<ActionResult> Engsection(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "EngBölməsi";
                ViewBag.Translate = localizer["English bölməsi (İB)"];
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;

                    }
                }
            }catch(Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
           
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/doctor")]
        public async Task<ActionResult> DoctorSection(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "Həkim Nəzarəti";
                ViewBag.Translate = localizer["Həkim nəzarəti"];
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;
                    }
                }
            }
            catch(Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/healthy")]
        public async Task<ActionResult> HealthySection(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "Sağlam Qida";
                ViewBag.Translate = localizer["Sağlam qida"];
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;
                    }
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/galarey")]
        public async Task<ActionResult> GalareyaSection()
        {
            string viewName = "Galareya";
            ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
            var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
            return View(photos);
        }

        [HttpGet]
        [Route("/{culture}/psyhological")]
        public async Task<ActionResult> PsychologicalSection(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "Psixalog Dəstəyi";
                ViewBag.Translate = localizer["Psixoloq dəstəyi"];
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;
                    }
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(resources);
        }


        [HttpGet]
        [Route("/{culture}/admission")]
        public async Task<ActionResult> RuleAdmission(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "Qəbul Qaydalari";
                ViewBag.Translate = localizer["Qəbul qaydaları"];
                culture = SupportedLanguage.KeyCulture(culture);

                int Id = dbContext.Languages.
                                     Single(x => x.Culture == culture).Id;

                ResourcesView resourcesView = await dbContext.ResourcesViews
                                                          .Where(x => x.Name == viewName)
                                                                       .FirstOrDefaultAsync();

                var photos = await dbContext.Photos
                                     .Where(x => x.ResourcesViewId == resourcesView.Id)
                                                        .ToListAsync();

                var data = dbContext.MainContents
                                   .Where(x => x.LanguageId == Id)
                                                               .ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;
                    }
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/security")]
        public async Task<ActionResult> SecuritySection(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "Təhlukəsizlik";
                ViewBag.Translate = localizer["Təhlükəsizlik"];
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;
                    }
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/association")]
        public async Task<ActionResult> AssociationSection(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "Dərnəklər";
                ViewBag.Translate = localizer["Dərnəklər"];
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;
                    }
                }
            }
            catch(Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/excursions")]
        public async Task<ActionResult> Excursions(string culture)
        {
            ResourcesViewModel resources = new ResourcesViewModel();
            try
            {
                string viewName = "Ekskusiyalar";
                ViewBag.Translate = localizer["Ekskursiyalar"];
                culture = SupportedLanguage.KeyCulture(culture);
                int Id = dbContext.Languages.Where(x => x.Culture == culture).FirstOrDefault().Id;
                ResourcesView resourcesView = await dbContext.ResourcesViews.Where(x => x.Name == viewName).FirstOrDefaultAsync();
                var photos = await dbContext.Photos.Where(x => x.ResourcesViewId == resourcesView.Id).ToListAsync();
                var data = dbContext.MainContents.Where(x => x.LanguageId == Id).ToList();
                resources.Photos = photos;
                foreach (var elm in data)
                {
                    if (elm.ResourcesViewId == resourcesView.Id)
                    {
                        resources.MainContent = elm;
                    }
                }
            }
            catch (Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(resources);
        }

        [HttpGet]
        [Route("/{culture}/contact")]
        public async Task<ActionResult> Contact(string culture)
        {
            ViewBag.Translate = localizer["Xəritə"];
            ViewBag.Translate1 = localizer["Azərbaycan, Bakı şəhəri, Həsən bəy Zərdabi küçəsi,"];
            await Task.Delay(0);
            return View();
        }

        [HttpGet]
        [Route("{culture}/story/{id}")]
        public async Task<ActionResult> Events(int? id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            var lang = requestCulture.RequestCulture.UICulture.Name;
            var culture = SupportedLanguage.GetUILanguage(lang);
            List<EventsAboutModel> eventsAboutModels = new List<EventsAboutModel>();
            try
            {
                if (!id.HasValue)
                    RedirectToAction(nameof(Index));

                var data = await dbContext.EventAbouts.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (data == null)
                    return BadRequest();

                int? cultureId = dbContext.Languages
                                                .AsNoTracking()
                                                      .Where(x => x.Culture == culture)
                                                                .First().Id;
                if (!cultureId.HasValue)
                    return NotFound();

                eventsAboutModels = await dbContext.EventAbouts.Where(x => x.Id == id)
                                                            .Include("EventAboutLanguage")
                                                                     .Include("EventAboutPhoto")
                                                                                   .Select(y => new EventsAboutModel
                                                                                   {
                                                                                       DateTime = y.DateTime,
                                                                                       Link = y.Link,
                                                                                       EventAboutLanguages = y.EventAboutLanguages.Where(x => x.LanguageId == cultureId).ToList(),
                                                                                       EventAboutPhotos = y.EventAboutPhotos,
                                                                                       EventId = y.Id
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
