using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class OrdersViewModel
    {
        public List<OrderViewModel> Orders{ get; set; }
        public int Count { get; set; }
    }
}