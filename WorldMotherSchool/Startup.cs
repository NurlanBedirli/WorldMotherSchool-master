using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorldMotherSchool.Areas.momsch.Core;
using WorldMotherSchool.Language;
using WorldMotherSchool.Models;

namespace WorldMotherSchool
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<SchoolDbContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<AppUser, IdentityRole>()
                           .AddDefaultTokenProviders()
                              .AddEntityFrameworkStores<SchoolDbContext>();

            // Configuration Localization
            services.AddLocalization(x => x.ResourcesPath = "Resources");


            services.Configure<RequestLocalizationOptions>(x =>
            {
                var langs = SupportedLanguage.GetSupportedLanguages()
                                        .Select(y => new CultureInfo(y))
                                            .ToList();
                x.SupportedCultures = langs;
                x.SupportedUICultures = langs;
                x.RequestCultureProviders.Insert(0, new SeqmentRequestCultureProvider());
            });

            services.AddAuthorization();
            services.AddAuthentication();

            services.AddDistributedMemoryCache();
            services.AddSession();


            services.AddMvc()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix,
                 op=> op.ResourcesPath = "Resources")
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IImageEnviroment, ImagesFile>();
            services.AddSingleton<IFileNameGenerator, DateTimeGenerator>();
            services.Configure<Option>(options => Configuration.Bind(options));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSession();
            app.UseRequestLocalization();
            app.UseStaticFiles();
            app.UseCookiePolicy();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "default_area",
                  template: "{area:exists}/{controller=Account}/{action=Login}/{id?}"
                );
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute("account", "{area}/home/",
                            defaults: new { controller = "home", action = "index" });
                routes.MapRoute("default_area", "{controller=Account}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("home", "{culture}/home/{*article}",
                            defaults: new { controller = "home", action = "index" });
                routes.MapRoute("defaultss", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaults",
                    template: "{culture}/{controller}/{action}/{id?}",
                    defaults: new { culture = "az", controller = "Home", action = "Index" });
            });

        }
    }
}
