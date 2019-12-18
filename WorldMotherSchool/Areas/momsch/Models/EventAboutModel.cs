using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Areas.momsch.Models
{
    public class EventAboutModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Baslig bosdur")]
        public string HeadAz { get; set; }
        [Required(ErrorMessage = "Baslig bosdur")]
        public string HeadEn { get; set; }
        [Required(ErrorMessage = "Baslig bosdur")]
        public string HeadRu { get; set; }
        [Required(ErrorMessage = "Tarix bosdur")]
        public DateTime DateTime { get; set; }
        public string Photo { get; set; }
        [Required(ErrorMessage = "Az dilinde Text hissesi bosdur")]
        [MinLength(350, ErrorMessage = "Textin uzunlugu 350 kicikdir")]
        public string TextAz { get; set; }
        [Required(ErrorMessage = "En dilinde Text hissesi bosdur")]
        [MinLength(350, ErrorMessage = "Textin uzunlugu 350 kicikdir")]
        public string TextEn { get; set; }
        [Required(ErrorMessage = "Ru dilinde Text hissesi bosdur")]
        [MinLength(350,ErrorMessage ="Textin uzunlugu 350 kicikdir")]
        public string TextRu { get; set; }
    }
}
