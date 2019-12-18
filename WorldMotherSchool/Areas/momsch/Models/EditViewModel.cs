using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldMotherSchool.Models;

namespace WorldMotherSchool.Areas.momsch.Models
{
    public class EditViewModel
    {
        public MainContent MainContent { get; set; }
        public List<Photo> Photos { get; set; } 
    }
}
