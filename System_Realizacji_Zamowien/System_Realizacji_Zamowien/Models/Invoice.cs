using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System_Realizacji_Zamowien.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public Order Order { get; set; }

        public ICollection<ItemCard> Positions{ get; set; }

        public DateTime Date { get; set; }

        public DateTime Term{ get; set; }
    }
}
