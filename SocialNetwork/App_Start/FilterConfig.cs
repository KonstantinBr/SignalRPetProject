using System.Web;
using System.Web.Mvc;
using SocialNetwork.Filters;
namespace SocialNetwork
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
