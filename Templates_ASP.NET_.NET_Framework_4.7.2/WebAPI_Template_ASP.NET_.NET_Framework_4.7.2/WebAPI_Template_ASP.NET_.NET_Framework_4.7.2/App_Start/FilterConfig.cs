using System.Web;
using System.Web.Mvc;

namespace WebAPI_Template_ASP.NET_.NET_Framework_4._7._2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
