using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Models
{
    public class SlideFigure
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Caption { get; set; }
        public string Image { get; set; }
        public Language Language { get; set; }
        public int LanguageId { get; set; }
    }
}
