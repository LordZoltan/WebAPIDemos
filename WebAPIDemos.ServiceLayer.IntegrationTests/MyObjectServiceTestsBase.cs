using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WebAPIDemos.ServiceLayer;

namespace WebAPIDemos.ServiceLayer.IntegrationTests
{
    //any service test done with a 'real' back-end - even if it's create specifically for the test
    //is an integration test and not a unit test.

    //this is where mocks are often used.  However, I often find that the difficulty in mocking, say, Entity Framework
    //(or a generic repository wrapping around EF) is more than setting up a transient database for your testing - 
    //not always, but often.

	[TestClass]
	public abstract class MyObjectServiceTestsBase : GenericServiceTestsBase<IMyObjectService>
	{
        protected virtual string GenerateNewObjectName()
        {
            return string.Format("Object {0}", Guid.NewGuid());
        }

		[TestMethod]
		public async Task ShouldInsertObject_AndSetID()
		{
            //how do we test? by checking that the returned object's ID is not the same
            var service = CreateService();
            string expectedName = GenerateNewObjectName();
            MyObject toInsert = new MyObject() { Name = expectedName };
            int originalID = toInsert.Id;
            await service.InsertMyObject(new SimpleCreateRequest<MyObject>(toInsert));
            Assert.AreNotEqual(originalID, toInsert.Id);
		}

        [TestMethod]
        public async Task ShouldInsertObject_ThenRetrieve()
        {

        }
	}
}
