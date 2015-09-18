using System.Web;
using System.Web.Mvc;

namespace WebAPIDemos.ServiceLayer.WebAPI.Server
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
