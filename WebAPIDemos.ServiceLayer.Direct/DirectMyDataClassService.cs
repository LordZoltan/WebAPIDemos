using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemos.Data;

namespace WebAPIDemos.ServiceLayer.Direct
{
    public class DirectMyDataClassService : IMyDataClassService
    {

			public Task<Data.MyDataClass> GetMyDataClass(int id)
			{
				//simulating the repo pattern here - but without a using.
				var repo = new ExampleRepo();
				return Task.Factory.StartNew(() => repo.MyDataClasses.Fetch(id));
			}

			public Task InsertMyDataClass(Data.MyDataClass obj)
			{
				var repo = new ExampleRepo();
				return Task.Factory.StartNew(() => repo.MyDataClasses.Insert(obj));
			}

			public Task<IQueryable<Data.MyDataClass>> All()
			{
				var repo = new ExampleRepo();
				return Task.Factory.StartNew(() => repo.MyDataClasses.All());
			}
		}
}
