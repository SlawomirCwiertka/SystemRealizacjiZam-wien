using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System_Realizacji_Zamowien.Models;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class ProductsViewModel
    {
       public List<Product> Products { get; set; }
       public int CountAll { get; set; }
       public int SetCount { get; set; }
    }
}