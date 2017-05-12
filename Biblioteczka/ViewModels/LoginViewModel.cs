﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteczka.ViewModels
{
    public class LoginViewModel
    {
        [Required]
       // [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
