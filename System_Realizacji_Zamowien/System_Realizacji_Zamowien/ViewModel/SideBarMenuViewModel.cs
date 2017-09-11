using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class SideBarMenuViewModel
    {
        public string MenuName { get; set; }
        public List<CategoryViewModel> SideBarMenu { get; set; }
    }
}