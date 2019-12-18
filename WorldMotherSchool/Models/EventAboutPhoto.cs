using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Models
{
    public class EventAboutPhoto
    {
        public int Id { get; set; }
        [Required]
        public string Photo { get; set; }
        public EventAbout EventAbout { get; set; }
        public int EventAboutId { get; set; }
    }
}
