using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Areas.momsch.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "UserName Or Email hissesi bosdur")]
        public string UserNameOrEmail { get; set; }
        [Required(ErrorMessage ="Password hissesi bosdur")]
        public string Password { get; set; }
    }
}
