using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string LinkPhoto { get; set; }
        
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public double Vat { get; set; }

        public int Availability { get; set; }
        
        public virtual Category Category { get; set; }

        public virtual ICollection<ItemCard> ShoppingCart { get; set; }
    }
}