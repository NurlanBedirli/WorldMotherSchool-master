using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Areas.momsch.Core
{
   public interface IFileNameGenerator
    {
        string GetFileName(string format);
    }
}
