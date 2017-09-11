using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System_Realizacji_Zamowien.Models
{
   public class ItemCard
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public bool InProgress { get; set; }

    }
}
