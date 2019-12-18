using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorldMotherSchool.Models;

namespace WorldMotherSchool.Core.SeedAsync
{
    public static class Seed
    {
        internal static async Task InvokeAsync(IServiceScope scope, SchoolDbContext dbContext)
        {
            bool users = await dbContext.Users.AnyAsync();
            if(!users)
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var role = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var hashedPassword = scope.ServiceProvider.GetRequiredService<IPasswordHasher<AppUser>>();

                AppUser user = new AppUser()
                {
                    FirstName = "Nurlan",
                    SecondName = "Bedirli",
                    UserName = "nurlanbli",
                    Email = "badirli222@gmail.com",
                };

                AppUser user1 = new AppUser()
                {
                    FirstName = "Sabina",
                    SecondName = "Mehdizade",
                    UserName = "sabina",
                    Email = "sabina.mehtizade123@gmail.com"
                };

                if (!dbContext.ResourcesViews.Any())
                {
                string[] view = new string[] {"Məqsədimiz","Məktəbin Yaranması","SchoolHymn","AzBölməsi", "RuBölməsi", "EngBölməsi",
                     "Dərnəklər","Ekskusiyalar","Həkim Nəzarəti","Psixalog Dəstəyi","Sağlam Qida","Təhlukəsizlik","Qəbul Qaydalari","Galareya"};

                    foreach (var elm in view)
                    {
                        ResourcesView resourcesView = new ResourcesView
                        {
                            Name = elm
                        };
                        await dbContext.ResourcesViews.AddAsync(resourcesView);
                        await dbContext.SaveChangesAsync();
                    }
                }

                if(!dbContext.Languages.Any())
                {
                    string[] langs = new string[] { "az", "en", "ru" };

                    foreach (var culture in langs)
                    {
                        WorldMotherSchool.Models.Language language = new WorldMotherSchool.Models.Language
                        {
                            Culture = culture
                        };
                        await dbContext.Languages.AddAsync(language);
                        await dbContext.SaveChangesAsync();
                    }
                }
                

                user.PasswordHash = hashedPassword.HashPassword(user, "12345");
                user1.PasswordHash = hashedPassword.HashPassword(user, "12345");

                IdentityResult result = await userManager.CreateAsync(user);
                IdentityResult result1 = await userManager.CreateAsync(user1);

                if (result.Succeeded && result1.Succeeded)
                {
                    string[] vs = new string[] { "Admin", "Moderator" };
                    foreach(string rol in vs)
                    {
                        IdentityResult identityResult = await role.CreateAsync(new IdentityRole { Name = rol });
                    }
                        await userManager.AddToRoleAsync(user, "Admin");
                        await userManager.AddToRoleAsync(user1, "Admin");
                }
            }
        }
    }
}
