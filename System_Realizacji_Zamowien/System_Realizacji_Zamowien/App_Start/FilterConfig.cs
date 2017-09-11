using System.Web;
using System.Web.Mvc;

namespace System_Realizacji_Zamowien
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
