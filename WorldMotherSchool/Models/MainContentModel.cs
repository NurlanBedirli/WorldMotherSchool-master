using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Models
{
    public class MainContentModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string Culture { get; set; }
        [Required]
        public string ViewName { get; set; }
    }
}
