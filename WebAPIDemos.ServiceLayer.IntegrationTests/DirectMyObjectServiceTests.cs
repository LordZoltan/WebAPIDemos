using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemos.ServiceLayer.IntegrationTests
{
    [TestClass]
    public class DirectMyObjectServiceTests : MyObjectServiceTestsBase
    {
        //NOTE - Integration tests should not be executed on a build server as part of a build or check-in policy.
        //You should have unit tests covering the interaction of a particular service instance with the back-end
        //repo - meaning, therefore, that the repository would be abstracted so that it could be mocked or otherwise
        //implemented with a stub or something like that.


        [ClassInitialize]
        public static void TestClassInit(TestContext testContext)
        {
            //ensure configuration is called
            //note - here it's called for this test class only.
            //if you have other tests you would do exactly the same to ensure that
            //Automapper is initialised before any test runs.

            //In an application, however, you'd only call Automapper's Initialize method once.
            AutoMapper.Mapper.Initialize(cfg => {
                Direct.AutoMapperConfiguration.Configure(cfg);
            });
        }

        protected override IMyObjectService CreateService()
        {
            return new Direct.MyObjectService();
        }
    }
}
