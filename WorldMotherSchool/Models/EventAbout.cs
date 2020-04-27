using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Models
{
    public class EventAbout
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public string Link { get; set; }
        public List<EventAboutLanguage> EventAboutLanguages { get; set; }
        public List<EventAboutPhoto> EventAboutPhotos { get; set; }
    }
}
