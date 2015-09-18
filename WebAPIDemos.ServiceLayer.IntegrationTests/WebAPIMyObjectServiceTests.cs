using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.IntegrationTests
{
    [TestClass]
    public class WebAPIMyObjectServiceTests : MyObjectServiceTestsBase
    {
        protected override IMyObjectService CreateService()
        {
            return new WebAPI.MyObjectService(new WebAPI.HttpRequestManager("localhost", 25564));
        }

        //In order for the Web API Tests to pass you will need to set the web project as a startup project - then [CTRL + F5]
        //to run it (if it's not already).  After that it'll work.
    }
}
