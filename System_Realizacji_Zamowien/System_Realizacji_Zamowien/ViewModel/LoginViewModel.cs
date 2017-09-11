using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Login musi posiadać min. {1} znaków")]
        public string Login { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Hasło musi posiadać min. {1} znaków")]
        public string Password { get; set; }
    }
}