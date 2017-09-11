using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Display(Name="Nazwa")]
        [Required]
        public string Name { get; set; }
        [Display(Name="Opis")]
        [Required]
        public string Description { get; set; }
        [Display(Name="Cena")]
        [Required]
        public decimal Price { get; set; }
        [Display(Name="Dostępne")]
        [Required]
        public int Availability { get; set; }
        public SelectList PrimaryCategories { get; set; }
        public string Photo { get; set; }
        [Required]
        [Display(Name="Kategoria")]
        public string SelectPrimaryCategory { get; set; }
        public SelectList UnitsOfMeasure { get; set; }
        [Required]
        [Display(Name="Jednostka miary")]
        public string SelectUnit { get; set; }
        [Required]
        [Display(Name="Vat")]
        public double Vat 
        { 
            get
            {
                return _vat * 100;
            }
            set
            {
                _vat = value;
            }
        }
        private double _vat; 
    }
}