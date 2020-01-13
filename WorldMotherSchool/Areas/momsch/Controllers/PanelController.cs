using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WorldMotherSchool.Areas.momsch.Core;
using WorldMotherSchool.Areas.momsch.Models;
using WorldMotherSchool.Models;

namespace WorldMotherSchool.Areas.momsch.Controllers
{
    [Authorize]
    [Area("momsch")]
    public class PanelController : Controller
    {
        public readonly SchoolDbContext schoolDb;
        public readonly IImageEnviroment imageEnviroment;
        public readonly IHostingEnvironment hostingEnviroment;
        public readonly IOptions<Option> options;
        public readonly IFileNameGenerator nameGenerator;

        public PanelController(SchoolDbContext _dbContext,
                               IImageEnviroment _imageEnviroment,
                               IHostingEnvironment _hostingEnvironment,
                               IOptions<Option> _options,
                               IFileNameGenerator _nameGenerator)
        {
            schoolDb = _dbContext;
            imageEnviroment = _imageEnviroment;
            hostingEnviroment = _hostingEnvironment;
            options = _options;
            nameGenerator = _nameGenerator;
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> PurposeView()
        {
            await Task.Delay(0);
            return View();
        }


        [HttpGet]
        [Route("/edit")]
        public ActionResult AddViewInformation()
        {
            return View();
        }

        [HttpPost]
        [Route("/edit")]
        public async Task<ActionResult> AddViewInformation(MainContentModel main)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    int? ViewId =  Convert.ToInt32(main.ViewName);
                    if (ViewId == null)
                        return BadRequest();

                    if(HasViewNameExistansions.IsSuchName(schoolDb, ViewId))
                    {

                        int? cultureId = schoolDb.Languages.Where(x => x.Culture == main.Culture).FirstOrDefault().Id;
                        if (!cultureId.HasValue)
                            return BadRequest();

                        var ViewName = HasViewNameExistansions.GetViewName(schoolDb, ViewId);
                        switch (ViewName)
                            {
                                case "Galareya":
                                ModelState.AddModelError("", $"Bu {ViewName} Sehifeye Elave etmek Olmaz");
                                return View();
                                case "SchoolHymn":
                                ModelState.AddModelError("", $"Bu {ViewName} Sehifeye Elave etmek Olmaz");
                                return View();
                           }

                        if (!HasViewNameExistansions.IsViewIdOrLanguageId(schoolDb, ViewId, cultureId))
                        {
                           await HasViewNameExistansions.AddElement(schoolDb, ViewId, cultureId, main.Text);
                            ModelState.AddModelError("", $"{ViewName} Sehifesine Elave Olundu");
                        }
                        else
                        {
                            ModelState.AddModelError("", $"Siz artiq  *{main.Culture}* dilinde **{ViewName}**  bolmesine elave etmisiz ");
                        }
                   
                    }
                    else
                    {
                    ModelState.AddModelError("", $"Bele Bolmə yoxdu");
                    }
                }
                catch(Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> AddEventAbout()
        {
            if (ModelState.IsValid)
            {

            }
            await Task.Delay(0);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddEventAbout(EventAbout eventAbout)
        {
            if(ModelState.IsValid)
            {

            }
            await Task.Delay(0);
            return View();
        }

        [HttpGet]
        [Route("/photo")]
        public async Task<ActionResult> AddPhoto(string[] photo)
        {
            await Task.Delay(0);
            return View();
        }


        [HttpPost]
        [Route("/photo")]
        public async Task<ActionResult> AddPhoto(Photo photos,IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                string ViewName = "";
                if (photos.ResourcesView != null)
                {
                    Photo currentPhoto = new Photo();
                    try
                    {
                        if (Image == null)
                            return View();
                        int? ViewId = Convert.ToInt32(photos.ResourcesView.Id);
                        if (ViewId == null)
                            return BadRequest();
                        if (HasViewNameExistansions.IsSuchName(schoolDb, ViewId))
                        {
                            ViewName = HasViewNameExistansions.GetViewName(schoolDb, ViewId);
                            switch (ViewName)
                            {
                                case "Sağlam Qida":
                                    currentPhoto = await schoolDb.Photos.Where(x => x.ResourcesViewId == ViewId).FirstOrDefaultAsync();
                                    if (currentPhoto != null)
                                    {
                                        fileName = await imageEnviroment.CreateImageAsync(Image, hostingEnviroment, nameGenerator);
                                        currentPhoto.Image = fileName;
                                        schoolDb.Photos.Update(currentPhoto);
                                        await schoolDb.SaveChangesAsync();
                                        ModelState.AddModelError("", $"Bu  {ViewName} yenilendi");
                                    }
                                    else
                                    {
                                        fileName = await imageEnviroment.CreateImageAsync(Image, hostingEnviroment, nameGenerator);
                                        Photo phot = new Photo
                                        {
                                            Image = fileName,
                                            ResourcesViewId = (int)ViewId
                                        };
                                        schoolDb.Photos.Add(phot);
                                        await schoolDb.SaveChangesAsync();
                                        ModelState.AddModelError("", $"Bu  {ViewName} sehifesi ucun elave edildi");
                                    }
                                    return View();
                            }
                            fileName =  await imageEnviroment.CreateImageAsync(Image, hostingEnviroment, nameGenerator);
                            Photo pht = new Photo
                            {
                                Image = fileName,
                                ResourcesViewId = (int)ViewId
                            };
                            schoolDb.Photos.Add(pht);
                            await schoolDb.SaveChangesAsync();
                            ModelState.AddModelError("", $"Bu {ViewName} səhifəsinə  səkil yükləndi");
                        }
                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("", exp.Message);
                    }
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> EditView(int? Id, string culture)
        {
            if (Id.HasValue)
            {
                var ViewName = HasViewNameExistansions.GetViewName(schoolDb, Id);
                if (string.IsNullOrEmpty(ViewName))
                    return RedirectToAction(nameof(AddViewInformation));

                var cultureValue = await schoolDb.Languages.Where(x => x.Culture == culture).FirstOrDefaultAsync();
                if(cultureValue != null)
                {
                    MainContent mainContent = new MainContent();
                    List<Photo> data;
                    switch (ViewName)
                    {
                        case "Galareya":
                              data = await schoolDb.Photos.Where(x => x.ResourcesViewId == Id).ToListAsync();
                            if (data.Count() == 0)
                                return RedirectToAction(nameof(AddViewInformation));
                            else
                                ViewBag.Photo = data;
                            return View(mainContent);
                        case "SchoolHymn":
                            data = await schoolDb.Photos.Where(x => x.ResourcesViewId == Id).ToListAsync();
                            if (data.Count() == 0)
                                return RedirectToAction(nameof(AddViewInformation));
                            else
                                ViewBag.Photo = data;
                            return View(mainContent);
                        case "HealthyFood":
                            return RedirectToAction(nameof(AddViewInformation));
                    }

                    if (HasViewNameExistansions.IsViewIdOrLanguageId(schoolDb,Id,cultureValue.Id))
                    {
                        options.Value.ViewId = (int)Id;
                        options.Value.CultureId = cultureValue.Id;
                        var contentData = await schoolDb.MainContents.Where(x => x.ResourcesViewId == Id).ToListAsync();
                        foreach(var elm in contentData)
                        {
                            if(elm.LanguageId == cultureValue.Id)
                            {
                                mainContent = elm;
                                break;
                            }
                        }
                        data =  await  schoolDb.Photos.Where(x => x.ResourcesViewId == Id).ToListAsync();
                        ViewBag.Photo = data;
                        
                        return View(mainContent);
                    }
                    else
                    {
                        return RedirectToAction(nameof(AddViewInformation));
                    }
                }
                else
                {
                    return RedirectToAction(nameof(AddViewInformation));
                }
            }
             return RedirectToAction(nameof(AddViewInformation));
        }


        [HttpPost]
        public async Task<ActionResult> EditView(MainContent mainContent)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Photo = await schoolDb.Photos.Where(x => x.ResourcesViewId == options.Value.ViewId).ToListAsync();
                if (mainContent != null)
                {
                    if (HasViewNameExistansions.IsViewIdOrLanguageId(schoolDb, options.Value.ViewId, options.Value.CultureId))
                    {
                        var data = await schoolDb.MainContents.Where(x => x.ResourcesViewId == options.Value.ViewId).ToListAsync();
                        foreach (var elm in data)
                        {
                            if (elm.LanguageId == options.Value.CultureId)
                            {
                                elm.LanguageId = options.Value.CultureId;
                                elm.ResourcesViewId = options.Value.ViewId;
                                elm.Text = mainContent.Text;
                                schoolDb.MainContents.Update(elm);
                                await schoolDb.SaveChangesAsync();
                                mainContent = elm;
                                ModelState.AddModelError("", "Elave Olundu");
                                break;
                            }
                        }
                        
                        return View(mainContent);
                    }
                }
            }
            return View();
        }


        [HttpGet]
        [Route("/slide")]
        public async Task<ActionResult> SlideImage()
        {
            var data = await schoolDb.SlideFigures.ToListAsync();
            if(data != null)
            {
                List<SlideFigureModel> models = new List<SlideFigureModel>();
                foreach(var elm in data)
                {
                    var culture = schoolDb.Languages.Where(x => x.Id == elm.LanguageId).FirstOrDefault().Culture;
                    SlideFigureModel figureModel = new SlideFigureModel()
                    {
                        Image = elm.Image,
                        Caption = elm.Caption,
                        Cultur = culture,
                        SlideFigureId = elm.Id
                    };
                    models.Add(figureModel);
                }
                ViewBag.SlideImage = models;
            }
            return View();
        }

        //Add slideimg
        [HttpPost]
        [Route("/slide")]
        public async Task<ActionResult> SlideImage(SlideFigure slideFigure,IFormFile Image)
        {
            if(ModelState.IsValid)
            {
                if(slideFigure != null)
                {
                    try
                    {
                        if(Image != null)
                        {
                            string FileName = await imageEnviroment.CreateImageAsync(Image, hostingEnviroment, nameGenerator);
                            var data = await schoolDb.Languages.Where(x => x.Culture == slideFigure.Language.Culture).FirstOrDefaultAsync();
                            if (data != null)
                            {
                                SlideFigure figure = new SlideFigure()
                                {
                                    Caption = slideFigure.Caption,
                                    Image = FileName,
                                    LanguageId = data.Id
                                };
                                await schoolDb.SlideFigures.AddAsync(figure);
                                await schoolDb.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Sekil hissesi bosdu");
                        }
                    }
                    catch(Exception exp)
                    {
                        ModelState.AddModelError("", exp.Message);
                    }
              
                }
            }
            return RedirectToAction(nameof(SlideImage));
        }



        [HttpGet]
        [Route("/delete/{id}")]
        public async Task<ActionResult> SlideDelete(int? id)
        {
            if(id != null)
            {
                var data = await schoolDb.SlideFigures.Where(x => x.Id == id).FirstOrDefaultAsync();
                if(data != null)
                {
                    if(imageEnviroment.DeleteImagePath(data.Image, hostingEnviroment))
                    {
                        schoolDb.SlideFigures.Remove(data);
                        await schoolDb.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(SlideImage));
        }



        [HttpGet]
        [Route("/addevent")]
        public async Task<ActionResult> EventsAbout()
        {
            await Task.Delay(0);
            return View();
        }



        [HttpPost]
        [Route("/addevent")]
        public async Task<ActionResult> EventsAbout(EventAboutModel eventAbouts,List<IFormFile> Photo)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if (Photo.Count != 0)
                    {
                        int? Azid = schoolDb.Languages.Where(x => x.Culture == "az").FirstOrDefault().Id;
                        if (!Azid.HasValue)
                            return NotFound();
                        int? Enid = schoolDb.Languages.Where(x => x.Culture == "en").FirstOrDefault().Id;
                        if (!Enid.HasValue)
                            return NotFound();
                        int? Ruid = schoolDb.Languages.Where(x => x.Culture == "ru").FirstOrDefault().Id;
                        if (!Ruid.HasValue)
                            return NotFound();

                        EventAbout eventAbout = new EventAbout
                        {
                            DateTime = eventAbouts.DateTime,
                        };
                        await schoolDb.EventAbouts.AddAsync(eventAbout);
                        await schoolDb.SaveChangesAsync();

                        foreach (var photo in Photo)
                        {
                            var fileName = await imageEnviroment.CreateImageAsync(photo, hostingEnviroment, nameGenerator);
                            EventAboutPhoto aboutPhoto = new EventAboutPhoto()
                            {
                                Photo = fileName,
                                EventAboutId = eventAbout.Id
                            };
                            await schoolDb.EventAboutPhotos.AddAsync(aboutPhoto);
                            await schoolDb.SaveChangesAsync();
                        }

                        EventAboutLanguage eventAboutLanguageaz = new EventAboutLanguage()
                        {
                            TextLang = eventAbouts.TextAz,
                            TextHead = eventAbouts.HeadAz,
                            LanguageId = (int)Azid,
                            EventAboutId = eventAbout.Id
                        };

                        EventAboutLanguage eventAboutLanguageen = new EventAboutLanguage()
                        {
                            TextLang = eventAbouts.TextEn,
                            TextHead = eventAbouts.HeadEn,
                            LanguageId = (int)Enid,
                            EventAboutId = eventAbout.Id
                        };

                        EventAboutLanguage eventAboutLanguageru = new EventAboutLanguage()
                        {
                            TextLang = eventAbouts.TextRu,
                            TextHead = eventAbouts.HeadRu,
                            LanguageId = (int)Ruid,
                            EventAboutId = eventAbout.Id
                        };
                        await schoolDb.EventAboutLanguages.AddAsync(eventAboutLanguageaz);
                        await schoolDb.EventAboutLanguages.AddAsync(eventAboutLanguageen);
                        await schoolDb.EventAboutLanguages.AddAsync(eventAboutLanguageru);
                        await schoolDb.SaveChangesAsync();
                        ModelState.AddModelError("", "Elave Olundu");
                        return View();
                    }
                    ModelState.AddModelError("", "Photo hissesi bosdur");
                }
                catch(Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            return View();
        }



        [HttpGet]
        [Route("/events")]
        public async Task<ActionResult> EventList()
        {
            List<EventListModel> eventAbouts = new List<EventListModel>();
            try
            {
              EventListModel eventList = new EventListModel();
                eventAbouts = await schoolDb.EventAbouts
                              .Include("EventAboutLanguage")
                                          .Select(x => new EventListModel
                {
                    DateTime = x.DateTime,
                    EventId = x.Id,
                    Head = x.EventAboutLanguages.First().TextHead
                }).ToListAsync();
            }
            catch(Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return View(eventAbouts);
        }


        [HttpGet]
        [Route("/editevent/{id}")]
        public async Task<ActionResult> EditEvents(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var data =  await schoolDb.EventAbouts.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data == null)
                return RedirectToAction(nameof(EventList));


            EventAboutModel eventAboutModel = new EventAboutModel();
            eventAboutModel.DateTime = data.DateTime;
            options.Value.EventAboutsId = (int)id;

            int? Azid = schoolDb.Languages.Where(x => x.Culture == "az").FirstOrDefault().Id;
            if (!Azid.HasValue)
                return NotFound();
            int? Enid = schoolDb.Languages.Where(x => x.Culture == "en").FirstOrDefault().Id;
            if (!Enid.HasValue)
                return NotFound();
            int? Ruid = schoolDb.Languages.Where(x => x.Culture == "ru").FirstOrDefault().Id;
            if (!Ruid.HasValue)
                return NotFound();


            var eventText =  await schoolDb.EventAboutLanguages.Where(x => x.EventAboutId == id).ToListAsync();
            foreach(var langtext in eventText)
            {
                if(langtext.LanguageId == (int)Azid)
                {
                    eventAboutModel.TextAz = langtext.TextLang;
                    eventAboutModel.HeadAz = langtext.TextHead;
                }
                if (langtext.LanguageId == (int)Enid)
                {
                    eventAboutModel.TextEn = langtext.TextLang;
                    eventAboutModel.HeadEn = langtext.TextHead;
                }
                if (langtext.LanguageId == (int)Ruid)
                {
                    eventAboutModel.TextRu = langtext.TextLang;
                    eventAboutModel.HeadRu = langtext.TextHead;
                }
            }
            var eventPhoto = await schoolDb.EventAboutPhotos.Where(x => x.EventAboutId == id).ToListAsync();
            ViewBag.Photo = eventPhoto;

            return View(eventAboutModel);
        }


        [HttpPost]
        [Route("/editevent/{id}")]
        public async Task<ActionResult> EditEvents(EventAboutModel eventAbouts, List<IFormFile> Photo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                        int? Azid = schoolDb.Languages.Where(x => x.Culture == "az").FirstOrDefault().Id;
                        if (!Azid.HasValue)
                            return NotFound();
                        int? Enid = schoolDb.Languages.Where(x => x.Culture == "en").FirstOrDefault().Id;
                        if (!Enid.HasValue)
                            return NotFound();
                        int? Ruid = schoolDb.Languages.Where(x => x.Culture == "ru").FirstOrDefault().Id;
                        if (!Ruid.HasValue)
                            return NotFound();

                        var eventAbout = await schoolDb.EventAbouts.Where(x => x.Id == options.Value.EventAboutsId).FirstOrDefaultAsync();
                        eventAbout.DateTime = eventAbouts.DateTime;

                        int count = 0;
                        foreach (var photo in Photo)
                        {
                            var fileName = await imageEnviroment.CreateImageAsync(photo, hostingEnviroment, nameGenerator);
                            EventAboutPhoto aboutPhoto = new EventAboutPhoto()
                            {
                                Photo = fileName,
                                EventAboutId = eventAbout.Id
                            };
                            await schoolDb.EventAboutPhotos.AddAsync(aboutPhoto);
                            await schoolDb.SaveChangesAsync();
                        }
                        if(Photo.Count != 0)
                        {
                        ModelState.AddModelError("", $"{count} Photo Elave Olundu");
                        }
                        var eventText = await schoolDb.EventAboutLanguages.Where(x => x.EventAboutId == options.Value.EventAboutsId).ToListAsync();
                        foreach (var langtext in eventText)
                        {
                            if (langtext.LanguageId == (int)Azid)
                            {
                                langtext.TextLang = eventAbouts.TextAz;
                                langtext.TextHead = eventAbouts.HeadAz;
                                schoolDb.EventAboutLanguages.Update(langtext);
                            }
                            if (langtext.LanguageId == (int)Enid)
                            {
                            langtext.TextLang = eventAbouts.TextEn;
                            langtext.TextHead = eventAbouts.HeadEn;
                            schoolDb.EventAboutLanguages.Update(langtext);
                            }
                            if (langtext.LanguageId == (int)Ruid)
                            {
                            langtext.TextLang = eventAbouts.TextRu;
                            langtext.TextHead = eventAbouts.HeadRu;
                            schoolDb.EventAboutLanguages.Update(langtext);
                            }
                        }
                        await schoolDb.SaveChangesAsync();
                        ModelState.AddModelError("", "Yenilendi");
                      return RedirectToAction("EditEvents", new { id = options.Value.EventAboutsId });
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            return RedirectToAction("EditEvents", new { id = options.Value.EventAboutsId });
        }

        [HttpGet]
        [Route("/eventsdel/{id}")]
        public async Task<ActionResult> DeletePhotoEventAbout(int? id)
        {
            if (!id.HasValue)
                return NotFound();
            var data = await schoolDb.EventAboutPhotos.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data == null)
                return BadRequest();

            imageEnviroment.DeleteImagePath(data.Photo, hostingEnviroment);
            schoolDb.EventAboutPhotos.Remove(data);
            await schoolDb.SaveChangesAsync();
            return RedirectToAction("EditEvents", new { id = options.Value.EventAboutsId });
        }
        
        [HttpGet]
        public async Task<ActionResult> DelEvents(int? id)
        {
            if(id.HasValue)
            {
                try
                {
                    var eventData = schoolDb.EventAbouts
                                 .Where(x => x.Id == id)
                                    .Include(x => x.EventAboutLanguages)
                                         .Include(y => y.EventAboutPhotos)
                                                         .FirstOrDefault();
                    if (eventData == null)
                        return BadRequest();

                    if(eventData.EventAboutPhotos.Count > 0)
                    {
                        foreach (var Evntphoto in eventData.EventAboutPhotos)
                        {
                            imageEnviroment.DeleteImagePath(Evntphoto.Photo, hostingEnviroment);
                        }
                    }
                    schoolDb.EventAbouts.Remove(eventData);
                    await schoolDb.SaveChangesAsync();
                }
                catch(Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
           
            }
            return RedirectToAction(nameof(EventList));
        }

    }

}