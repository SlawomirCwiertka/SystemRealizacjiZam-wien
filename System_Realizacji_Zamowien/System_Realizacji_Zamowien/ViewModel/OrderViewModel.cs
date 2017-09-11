using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System_Realizacji_Zamowien.Models;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public virtual List<ItemCardViewModel> Items { get; set; }
        [Display(Name="Data")]
        public DateTime DataOrder { get; set; }
        public List<Invoice> Invoices { get; set; }
        public bool Success { get; set; }
        [Display(Name="Czas do zapłaty")]
        public int TimeToPay { get; set; }
        public User User { get; set; }
        public bool IsRead { get; set; }
    }
}