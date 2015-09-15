using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemos.Data;
using WebAPIDemos.ServiceLayer.Abstractions;

namespace WebAPIDemos.ServiceLayer.Direct
{
    public class DirectMyDataClassService : IMyObjectService
    {
        public Task<MyObject> GetMyObject(int id)
        {
            //simulating the repo pattern here - but without a using.
            var repo = new ExampleRepo();
            return Task.Factory.StartNew(() => repo.MyDataClasses.Fetch(id));
        }

        public Task InsertMyDataClass(MyObject obj)
        {
            var repo = new ExampleRepo();
            return Task.Factory.StartNew(() => repo.MyDataClasses.Insert(obj));
        }

        public Task<IQueryable<MyObject>> All()
        {
            var repo = new ExampleRepo();
            return Task.Factory.StartNew(() => repo.MyDataClasses.All());
        }
    }
}
