using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Realizacji_Zamowien.Models;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class RegistrationViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Login musi posiadać min. {1} znaków")]
        [Display(Name="Login")]
        public string Login { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Imie musi posiadać min. 3 litery")]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Login musi posiadać min. 5 znaków")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Hasło musi posiadać min. 5 znaków")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        public SelectList Accesses { get; set; }
        [Required]
        [Display(Name = "Uprawnienia")]
        public string SelectedAccess { get; set; }
        public SelectList Firms { get; set; }
        [Required]
        [Display(Name = "Firma")]
        public string SelectedFirm { get; set; }

    }
}