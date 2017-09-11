using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual Category Parent { get; set; }
        public string LinkPhoto { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }
    }
}