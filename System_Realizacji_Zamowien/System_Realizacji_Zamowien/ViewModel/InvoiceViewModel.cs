using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System_Realizacji_Zamowien.Models;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        [Display(Name="Sprzedawca")]
        public Firm Dealer { get; set; }
        [Display(Name="Pozycje")]
        public List<ItemCardViewModel> Positions { get; set; }
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        [Display(Name="Termin")]
        public DateTime Term { get; set; }
        [Display(Name = "Użytkownik")]
        public SmallUserViewModel User { get; set; }
        [Display(Name = "Razem netto")]
        public string SumNetto
        {
            get
            {
                return String.Format("{0:C}", Positions.Sum(x => x._variableNetto));
            }
        }
        [Display(Name = "Razem brutto")]
        public string SumBrutto
        {
            get
            {
                return String.Format("{0:C}", Positions.Sum(x => x._variableNetto + x._variableNetto* (decimal)x.Product.Vat));
            }
        }
    }
}