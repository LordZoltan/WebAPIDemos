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
        public async Task ShouldReturnUnsuccessfulForMissingObject()
        {
            //controlled environment here - we know that no object will ever be given -1 as an ID
            var service = CreateService();
            //note - another way to do this is to have another result type for 'Get' operations which adds
            //a 'NotFound' property (see XMLDoc comments on the IMyObjectService.GetMyObject method for more).
            var result = await service.GetMyObject((-1).AsServiceRequest());
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.ErrorMessage);
            Assert.IsNull(result.Exception);
        }

        [TestMethod]
        public async Task ShouldInsertObject_AndSetID()
        {
            //how do we test? by checking that the returned object's ID is not the same
            var service = CreateService();
            string expectedName = GenerateNewObjectName();
            MyObject toInsert = new MyObject() { Name = expectedName };
            int originalID = toInsert.Id;

            var result = await service.InsertMyObject(toInsert.AsServiceRequest());
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success, result.ErrorMessage);
            Assert.AreNotEqual(originalID, result.Result);
        }

        [TestMethod]
        public async Task ShouldInsertObject_ThenRetrieve()
        {
            //being an integration test - this is similar to the above test - except we're also testing that we can retrieve
            //what we've inserted by the id that is returned.
            var service = CreateService();
            string expectedName = GenerateNewObjectName();
            MyObject toInsert = new MyObject() { Name = expectedName };
            var result = await service.InsertMyObject(toInsert.AsServiceRequest());
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success, result.ErrorMessage);
            var retrieved = await service.GetMyObject(result.Result.AsServiceRequest());
            Assert.IsTrue(retrieved.Success);
            Assert.AreEqual(expectedName, retrieved.Result.Name);
        }
	}
}
