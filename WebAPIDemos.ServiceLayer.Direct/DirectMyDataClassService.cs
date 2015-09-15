using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIDemos.Data;

namespace WebAPIDemos.ServiceLayer.Direct
{
    public class DirectMyDataClassService : IMyObjectService
    {
        public Task<MyObject> GetMyObject(int id)
        {
            //simulating the repo pattern here - but without a using.
            return Task.Factory.StartNew(() => {
                var repo = new ExampleRepo();
                return Mapper.Map<MyObject>(repo.MyEntities.Fetch(id));
            });
        }

        public Task InsertMyObject(MyObject obj)
        {
            return Task.Factory.StartNew(() => {
                var repo = new ExampleRepo();
                //interesting one this - you expect your repo to assign the ID to the object - so here we
                //map from MyObject to MyEntity, then, after insertion, we map the inserted entity back to 
                //the obj
                var toInsert = Mapper.Map<MyEntity>(obj);
                repo.MyEntities.Insert(toInsert);
                Mapper.Map(toInsert, obj);
            });
        }


        public Task<IQueryable<MyObject>> All()
        {
            //in our example, we can do this - but, if the context is disposable, then
            //tasks that return IQueryables are not ideal - because lifetime management of the 
            //context (and therefore its database connection) is not possible unless you are 
            //creating the contexts per-request.
            return Task.Factory.StartNew(() => {
                var repo = new ExampleRepo();
                repo.MyEntities.All()
            });
        }
    }
}
