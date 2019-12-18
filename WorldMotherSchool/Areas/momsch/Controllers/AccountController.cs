using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldMotherSchool.Areas.momsch.Core;
using WorldMotherSchool.Areas.momsch.Core.SessionExistansions;
using WorldMotherSchool.Areas.momsch.Models;
using WorldMotherSchool.Models;

namespace WorldMotherSchool.Areas.momsch.Controllers
{
    
    [Area("momsch")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IPasswordHasher<AppUser> passwordHasher;
        private readonly SignInManager<AppUser> signInManager;
        private readonly SchoolDbContext dbContext;
        private readonly IImageEnviroment imageEnviroment;
        private readonly IHostingEnvironment hostingEnviroment;
        private readonly IFileNameGenerator nameGenerator;


        public AccountController(UserManager<AppUser> _userManager,
                                       IPasswordHasher<AppUser> _passwordHasher,
                                               SignInManager<AppUser> _signInManager,
                                                                    SchoolDbContext _dbContext,
                                                                           IImageEnviroment _imageEnviroment,
                                                                                  IHostingEnvironment _hostingEnviroment,
                                                                                         IFileNameGenerator _nameGenerator)
        {
            userManager = _userManager;
            passwordHasher = _passwordHasher;
            signInManager = _signInManager;
            dbContext = _dbContext;
            imageEnviroment = _imageEnviroment;
            hostingEnviroment = _hostingEnviroment;
            nameGenerator = _nameGenerator;
        }


        [Authorize]
        [Route("/anasehife")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/momsch")]
        public async Task<ActionResult> Login()
        {
            await Task.Delay(0);
            return View();
        }

        [HttpPost]
        [Route("/momsch")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
               AppUser user = null;
                user =  await userManager.FindByEmailAsync(model.UserNameOrEmail);
                if(user == null)
                {
                    user = await userManager.FindByNameAsync(model.UserNameOrEmail);
                }

                if(user != null)
                {
                    PasswordVerificationResult passwordVerification = passwordHasher
                                    .VerifyHashedPassword(user, user.PasswordHash, model.Password);

                    if(passwordVerification == PasswordVerificationResult.Success)
                    {
                        SessionUserModel sessionUser = new SessionUserModel
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            SecondName = user.SecondName,
                            Photo = user.Photo
                        };
                        HttpContext.Session.SetObjectAsJson("User", sessionUser);
                        await signInManager.SignInAsync(user, true);
                        return RedirectToAction("Index", "Account", new { area = "momsch" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Password Yalnisdir");
                        return View();
                    }
                }
                ModelState.AddModelError("", $"Belə bir {model.UserNameOrEmail} şəxs qeydiyyatdan keçməyib");
            }
            return View();
        }


        [HttpGet]
        [Authorize]
        [Route("/register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("/register")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel register)
        {
            if(ModelState.IsValid)
            {

                AppUser user = null;
                try
                {
                    user = await userManager.FindByEmailAsync(register.Email);
                    if (user == null)
                    {
                        user = await userManager.FindByNameAsync(register.UserName);
                    }

                    if (user == null)
                    {
                        AppUser appUser = new AppUser();
                        appUser.Email = register.Email;
                        appUser.UserName = register.UserName;
                        appUser.FirstName = register.FirstName;
                        appUser.SecondName = register.SecondName;
                        appUser.PasswordHash = passwordHasher.HashPassword(appUser, register.Password);

                        IdentityResult result = await userManager.CreateAsync(appUser);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(appUser, "Moderator");
                        }
                        return RedirectToAction(nameof(Login));
                    }
                    ModelState.AddModelError("", "Bele bir User ve ya UserName artiq movcuddur");
                }
                catch(Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            return View();
        }

        [HttpGet]
        [Route("/roleuser")]
        public async Task<ActionResult> ListRoleUser()
        {
            List<RoleUserModel> roles = new List<RoleUserModel>();
            try
            {
                var users = await userManager.GetUsersInRoleAsync("Moderator");
                foreach (var user in users)
                {
                    RoleUserModel roleUser = new RoleUserModel()
                    {
                        FirstName = user.FirstName,
                        SecondName = user.SecondName,
                        Email = user.Email,
                        RoleId = user.Id,
                        Role = "Moderator"
                    };
                    roles.Add(roleUser);
                }
            }
            catch(Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
          
            return View(roles);
        }

        [HttpGet]
        [Route("/deleteuser/{page}")]
        public async Task<ActionResult> UserDelete(string page)
        {
            try
            {
                if (string.IsNullOrEmpty(page))
                    return RedirectToAction(nameof(ListRoleUser));
                var user = await userManager.FindByIdAsync(page);
                if (user == null)
                    return RedirectToAction(nameof(ListRoleUser));
                await userManager.DeleteAsync(user);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(ListRoleUser));
            }
            catch(Exception exp)
            {
                ModelState.AddModelError("", exp.Message);
            }
            return RedirectToAction(nameof(ListRoleUser));
        }


        [HttpGet]
        [Route("/edit/{id}")]
        public async Task<ActionResult> EditProfile( string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));


            var data =  await userManager.FindByIdAsync(id);
            if(data == null)
                return RedirectToAction(nameof(Index));

            EditUserModel userModel = new EditUserModel
            {
                FirstName = data.FirstName,
                SecondName = data.SecondName,
                UserName = data.UserName,
                Email = data.Email,
            };
            return View(userModel);
        }

        [HttpPost]
        [Route("/edit/{id}")]
        public async Task<ActionResult> EditProfile(EditUserModel userModel,IFormFile Photo)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(userModel.Email))
                        return RedirectToAction(nameof(EditProfile));

                    var data = await userManager.FindByEmailAsync(userModel.Email);
                    if (data != null)
                    {
                        if (userModel.ComparePassword != null)
                        {
                            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(data, data.PasswordHash, userModel.ComparePassword);
                            if (result == PasswordVerificationResult.Success)
                            {
                                if (Photo != null)
                                {
                                    if (userModel.Password != null)
                                    {
                                        var fileName = await imageEnviroment.CreateImageAsync(Photo, hostingEnviroment, nameGenerator);
                                        imageEnviroment.DeleteImagePath(data.Photo, hostingEnviroment);
                                        var hashed = passwordHasher.HashPassword(data, userModel.Password);
                                        data.Photo = fileName;
                                        data.FirstName = userModel.FirstName;
                                        data.SecondName = userModel.SecondName;
                                        data.UserName = userModel.UserName;
                                        data.PasswordHash = hashed;
                                        SessionUserModel sessionUser = new SessionUserModel
                                        {
                                            Id = data.Id,
                                            FirstName = data.FirstName,
                                            SecondName = data.SecondName,
                                            Photo = data.Photo
                                        };
                                        HttpContext.Session.SetObjectAsJson("User", sessionUser);
                                        await userManager.UpdateAsync(data);
                                        await dbContext.SaveChangesAsync();
                                        ModelState.AddModelError("", "Password ve Parol Elave Olundu");
                                        return View();
                                    }
                                }
                                else
                                {
                                    if (userModel.Password != null)
                                    {
                                        var hashed = passwordHasher.HashPassword(data, userModel.Password);
                                        data.FirstName = userModel.FirstName;
                                        data.SecondName = userModel.SecondName;
                                        data.UserName = userModel.UserName;
                                        data.PasswordHash = hashed;
                                        SessionUserModel sessionUser = new SessionUserModel
                                        {
                                            Id = data.Id,
                                            FirstName = data.FirstName,
                                            SecondName = data.SecondName,
                                            Photo = data.Photo
                                        };
                                        HttpContext.Session.SetObjectAsJson("User", sessionUser);
                                        await userManager.UpdateAsync(data);
                                        await dbContext.SaveChangesAsync();
                                        ModelState.AddModelError("", "Password Elave Olundu");
                                        return View();
                                    }
                                    ModelState.AddModelError("", "Yeni Password Bosdur");
                                    return View();
                                }
                            }
                            ModelState.AddModelError("", "Kohne passwordu duzgun qeyd edin");
                        }
                        else
                        {
                            if (Photo != null)
                            {
                                var fileName = await imageEnviroment.CreateImageAsync(Photo, hostingEnviroment, nameGenerator);
                                imageEnviroment.DeleteImagePath(data.Photo, hostingEnviroment);
                                data.Photo = fileName;
                                data.FirstName = userModel.FirstName;
                                data.SecondName = userModel.SecondName;
                                data.UserName = userModel.UserName;
                                SessionUserModel sessionUser = new SessionUserModel
                                {
                                    Id = data.Id,
                                    FirstName = data.FirstName,
                                    SecondName = data.SecondName,
                                    Photo = data.Photo
                                };
                                HttpContext.Session.SetObjectAsJson("User", sessionUser);
                                await userManager.UpdateAsync(data);
                                await dbContext.SaveChangesAsync();
                                ModelState.AddModelError("", "Photo Elave Olundu");
                                return View();
                            }
                            else
                            {
                                data.FirstName = userModel.FirstName;
                                data.SecondName = userModel.SecondName;
                                data.UserName = userModel.UserName;
                                SessionUserModel sessionUser = new SessionUserModel
                                {
                                    Id = data.Id,
                                    FirstName = data.FirstName,
                                    SecondName = data.SecondName,
                                    Photo = data.Photo
                                };
                                HttpContext.Session.SetObjectAsJson("User", sessionUser);
                                await userManager.UpdateAsync(data);
                                await dbContext.SaveChangesAsync();
                                ModelState.AddModelError("", "Elave Olundu");
                                return View();
                            }
                        }
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
        [Authorize]
        [Route("/logout")]
        public async Task<ActionResult> LogOut()
        {
            await  signInManager.SignOutAsync();
            return RedirectToAction("Login","Account",new {area = "momsch" });
        }

    }
}