using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAPIDemos.ServiceLayer;

namespace WebAPIDemos.ExampleApp.Controllers
{
    public class HomeController : Controller
    {
        IMyObjectService _myObjectService = new ServiceLayer.WebAPI.MyObjectService(new ServiceLayer.WebAPI.HttpRequestManager("localhost", 25564));

        public async Task<ActionResult> Index()
        {
            var response = await _myObjectService.GetMyObject((1).AsServiceRequest());

            var viewModel = new Models.HomeIndexViewModel() { MyObject = response.Result };
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}