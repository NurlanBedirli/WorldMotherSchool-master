using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Models
{
    public class SchoolDbContext : IdentityDbContext<AppUser>
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> dbContext) : base(dbContext) { }

        public DbSet<ResourcesView> ResourcesViews { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<SlideFigure> SlideFigures { get; set; }
        public DbSet<EventAbout> EventAbouts { get; set; }
        public DbSet<EventAboutLanguage> EventAboutLanguages { get; set; }
        public DbSet<EventAboutPhoto> EventAboutPhotos { get; set; }
        public DbSet<MainContent> MainContents { get; set; }
        public DbSet<Language> Languages { get; set; }
        //public DbSet<AzSection> AzSections { get; set; }
        //public DbSet<DoctorControl> DoctorControls { get; set; }
        //public DbSet<EngSection> EngSections { get; set; }
        //public DbSet<Excursions> Excursions { get; set; }
        //public DbSet<HealthyFood> HealthyFoods { get; set; }
        //public DbSet<PhotoGalareya> PhotoGalareyas { get; set; }
        //public DbSet<Psychological> Psychologicals { get; set; }
        //public DbSet<RuleAdmission> RuleAdmissions { get; set; }
        //public DbSet<RuSection> RuSections { get; set; }
        //public DbSet<Security> Securities { get; set; }
    }
}
