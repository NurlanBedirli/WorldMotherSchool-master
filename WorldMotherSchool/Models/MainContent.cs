using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Models
{
    public class MainContent
    {
        public int Id { get; set; }
        [Required]
        [MaxLength]
        public string Text { get; set; }
        public Language Language { get; set; }
        public int LanguageId { get; set; }
        public ResourcesView ResourcesView { get; set; }
        public int ResourcesViewId { get; set; }
    }
}
