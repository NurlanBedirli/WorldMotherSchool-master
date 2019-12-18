using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public ResourcesView ResourcesView { get; set; }
        public int ResourcesViewId { get; set; }
    }
}
