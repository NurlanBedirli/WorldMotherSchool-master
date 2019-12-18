using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Models
{
    public class HealthyFood
    {
        public int Id { get; set; }
        public string Head { get; set; }
        public ResourcesView ResourcesView { get; set; }
        public int ResourcesViewId { get; set; }
        public Language Language { get; set; }
        public int LanguageId { get; set; }
    }
}
