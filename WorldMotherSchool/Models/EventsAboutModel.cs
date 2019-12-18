using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Models
{
    public class EventsAboutModel
    {
        public int EventId { get; set; }
        public DateTime DateTime { get; set; }
        public List<EventAboutLanguage> EventAboutLanguages { get; set; }
        public List<EventAboutPhoto> EventAboutPhotos { get; set; }
    }
}
