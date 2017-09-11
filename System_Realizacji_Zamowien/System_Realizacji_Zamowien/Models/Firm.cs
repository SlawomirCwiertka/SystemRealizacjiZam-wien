using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.Models
{
    public class Firm
    {
        public int Id { get; set; }
        [Display(Name="Nazwa")]
        public string Name { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Ulica i nr.")]
        public string Adress { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }
        [Display(Name = "Nip")]
        public string Nip { get; set; }
        [Display(Name = "Regon")]
        public string Regon { get; set; }
        public ICollection<User> Employes{ get; set; }
    }
}