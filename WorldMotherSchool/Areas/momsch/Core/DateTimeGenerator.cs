using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Areas.momsch.Core
{
    public class DateTimeGenerator : IFileNameGenerator
    {
        public string GetFileName(string format)
        {
            return $"{Path.GetRandomFileName()}.{DateTime.Now.ToString("dd.mmmm.yy")}.{format}";
        }
    }
}
