using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System_Realizacji_Zamowien.Models;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class ItemCardViewModel
    {
        public int Id { get; set; }
        [Display(Name="Ilość")]
        public int Count { get; set; }
        public Product Product { get; set; }
        public virtual User User { get; set; }
        [Display(Name = "Do faktury")]
        public int ToInvoice { get; set; }
        [Display(Name="Nazwa produktu")]
        public string ProductName
        {
            get
            {
                return Product.Name;
            }
        }
        public decimal _variableNetto
        {
            get
            {
                return this.Product.Price * this.Count;
            }
        }
        [Display(Name = "Wartość netto")]
        public string VariableNetto
        {
            get
            {
                return String.Format("{0:C}", _variableNetto);
            }
        }
        [Display(Name="Netto")]
        public decimal Netto
        {
            get
            {
                return this.Product.Price;
            }
        }
        [Display(Name = "Wartość brutto")]
        public string VariableBrutto
        {
            get
            {
                return String.Format("{0:C}", this._variableNetto + this._variableNetto * (decimal)this.Product.Vat);

            }
        }
        [Display(Name = "Vat")]
        public string Vat
        {
            get
            {
                return String.Format("{0:C}", this._variableNetto * (decimal)this.Product.Vat);
            }
        }
        [Display(Name = "% Vat")]
        public string ProcentVat
        {
            get
            {
                return String.Format("{0}%", this.Product.Vat * 100);
            }
        }
        [Display(Name = "Jednostka miary")]
        public string UnitOfMeasure { get; set; }
    }
}