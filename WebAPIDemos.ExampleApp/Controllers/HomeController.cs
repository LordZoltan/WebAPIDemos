using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAPIDemos.ExampleApp.Classes;
using WebAPIDemos.ExampleApp.Models;
using WebAPIDemos.ServiceLayer;

namespace WebAPIDemos.ExampleApp.Controllers
{
    public class HomeController : Controller
    {
        //demonstrating use of the caching object service
        IMyObjectService _myObjectService = new MyCachingObjectService(
            new ServiceLayer.WebAPI.MyObjectService(new ServiceLayer.WebAPI.HttpRequestManager("localhost", 25564)));

        //new MyCachingObjectService(
            //new ServiceLayer.WebAPI.MyObjectService(new ServiceLayer.WebAPI.HttpRequestManager("localhost", 25564)));

        public async Task<ActionResult> Index(int? pageNumber)
        {
            IServiceResponse lastResponse = (IServiceResponse)TempData["LastResponse"];
            var response = await _myObjectService.QueryMyObjects(new PagedQuery() { Page = pageNumber ?? 1 }.AsServiceRequest());
            //we should be doing some more advanced handling of API service errors.  See the MyObjects action for more.
            if (response.ErrorMessage != null)
                return View("ApiError", response);

            var viewModel = new Models.HomeIndexViewModel() { Results = Mapper.Map<PagedResult<MyObjectModel>>(response.Result), LastOperationResponse = lastResponse };
            if (viewModel.Results == null) //
                viewModel.Results = new PagedResult<MyObjectModel>();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(MyObjectModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var response = await _myObjectService.InsertMyObject(Mapper.Map<MyObject>(model).AsServiceRequest());
            //yes - does require session (not my preferred thing to do - the less we use session the better; ideally: not at all)
            TempData["LastResponse"] = response;
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            var response = await _myObjectService.GetMyObject(id.AsServiceRequest());

            // demonstrates some more advanced handling of errors from critical operations.
            if (!response.Success)
            {
                if(response.Exception != null)
                    throw new HttpException(response.ErrorMessage ?? "The API operation failed with an exception", response.Exception);
                else if(response.ErrorMessage != null)
                    throw new HttpException(response.ErrorMessage);

                return HttpNotFound();
            }
            else
            {
                return View(Mapper.Map<MyObjectModel>(response.Result));
            }
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