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
        [ClassInitialize]
        public static void TestClassInit(TestContext context)
        {
            //ensure configuration is called
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
