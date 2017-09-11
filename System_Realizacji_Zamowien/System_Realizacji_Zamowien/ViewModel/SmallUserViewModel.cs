using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class SmallUserViewModel
    {
        public int Id { get; set; }
        [Display(Name="Nick")]
        public string Login { get; set; }
        [Display(Name="Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Firma")]
        public string FirmName { get; set; }
        [Display(Name = "Nip")]
        public string FirmNip { get; set; }
        [Display(Name = "Regon")]
        public string FirmRegon { get; set; }
        [Display(Name = "Adress")]
        public string FirmAdress { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string FirmPostCode { get; set; }
    }
}