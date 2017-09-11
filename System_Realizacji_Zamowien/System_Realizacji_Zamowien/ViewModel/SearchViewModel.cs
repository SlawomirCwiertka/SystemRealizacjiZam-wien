using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System_Realizacji_Zamowien.Models;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class SearchViewModel
    {
        public List<Category> PrimaryCategories { get; set; }
        public SelectList SubCategories { get; set; }
        public string SearchText { get; set; }
        public string SelectedSubCategories { get; set; }
        public string SelectedPrimaryCategory { get; set; }
    }
}