using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name= "Nazwa")]
        public string Name { get; set; }
        public List<CategoryViewModel> SubCategories { get; set; }
        [Display(Name = "Kategorie")]
        public SelectList Categories{ get; set; }
        [Required]
        [Display(Name="Nadkategoria")]
        public string ParentCategory { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }
    }
}