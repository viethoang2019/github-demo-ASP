using System.Web;
using System.Web.Mvc;

namespace FrameWorkASPwithADO_Photo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
