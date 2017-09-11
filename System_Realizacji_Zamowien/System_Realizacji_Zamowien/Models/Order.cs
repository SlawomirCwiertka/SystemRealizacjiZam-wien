using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual ICollection<ItemCard> Items { get; set; }
        public DateTime DataOrder { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public bool Success { get; set; }
        public User User { get; set; }
        public ICollection<State> States{ get; set; }
    }
}