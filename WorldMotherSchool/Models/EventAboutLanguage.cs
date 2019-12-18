using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Models
{
    public class EventAboutLanguage
    {
        public int Id { get; set; }
        [Required]
        public string TextLang { get; set; }
        [Required]
        public string TextHead { get; set; }
        public Language Language { get; set; }
        public int LanguageId { get; set; }
        public EventAbout EventAbout { get; set; }
        public int EventAboutId { get; set; }
    }
}
