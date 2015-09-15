using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIDemos.Data;
using System.Linq;

namespace WebAPIDemos.Tests
{
	[TestClass]
	public class RepoTests
	{
		[TestMethod]
		public void ShouldAddAndRetrieveObject()
		{
			var repo = new ExampleRepo();
			var toAdd = new MyEntity() { Name = "Hello World" };
			repo.MyDataClasses.Insert(toAdd);

			//must assign id
			Assert.AreNotEqual(0, toAdd.Id);
			//must retrieve by that ID
			Assert.AreSame(toAdd, repo.MyDataClasses.Fetch(toAdd.Id));
		}

		[TestMethod]
		public void ShouldQuery()
		{
			//now, of course, the IQueryable implementation here is completely bobbins.  But, it
			//just needs to be able to demonstrate the filtering is actually possible.

			var stringPrefix = DateTime.UtcNow.Ticks.ToString();

			var repo = new ExampleRepo();
			foreach (var obj in Enumerable.Range(0, 5).Select(i => new MyEntity() { Name = string.Format("{0}: Object {1}", stringPrefix, i + 1.ToString()) }))
			{
				repo.MyDataClasses.Insert(obj);
			}

			var firstSearch = repo.MyDataClasses.All().Where(o => o.Name.StartsWith(stringPrefix)).Count();
			Assert.AreEqual(5, firstSearch);
		}
	}
}
